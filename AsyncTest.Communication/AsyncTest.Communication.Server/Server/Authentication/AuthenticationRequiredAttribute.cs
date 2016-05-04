using System;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using AsyncTest.Communication.Interface.Authentication;
using AsyncTest.Communication.Server.Database.Authentication;
using AsyncTest.Shared;

namespace AsyncTest.Communication.Server.Server.Authentication
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthenticationRequiredAttribute : Attribute, IAuthenticationFilter
    {
        private const long RequestTimeDelta = 5;

        private readonly IClientRepository _clientRepository;

        public AuthenticationRequiredAttribute()
        {
            _clientRepository = MainKernel.Get<IClientRepository>();
        }

        public bool AllowMultiple => true;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            if (context.Request.Headers.Authorization == null)
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                return;
            }

            if (!AuthenticationValues.AuthenticationScheme.Equals(context.Request.Headers.Authorization.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                return;
            }

            string rawHeader = context.Request.Headers.Authorization.Parameter;
            string[] headers = rawHeader.Split(':');

            if (headers.Length != 4)
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                return;
            }

            long timestamp;
            if (!long.TryParse(headers[3], out timestamp))
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                return;
            }

            string appId = headers[0];
            string receivedSignature = headers[1];
            string nonce = headers[2];
            Guid appIdGuid;

            if (!Guid.TryParse(appId, out appIdGuid))
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                return;
            }

            ClientDto client = await _clientRepository.FindAsync(appIdGuid).ConfigureAwait(false);
            if (client == null)
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                return;
            }

            if (IsReplayRequest(nonce, timestamp))
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                return;
            }

            string contentHash = await AuthenticationHelper.ComputeContentHashAsync(context.Request.Content).ConfigureAwait(false);
            string signatureData = AuthenticationHelper.CreateSignatureData(appId, context.Request, timestamp, nonce, contentHash);
            string signature = AuthenticationHelper.CalculateHMACSHA256(client.SharedSecret, signatureData);

            if (!receivedSignature.Equals(signature, StringComparison.Ordinal))
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                return;
            }

            context.Principal = new GenericPrincipal(new GenericIdentity(appId), null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            context.Result = new ResultWithChallenge(context.Result);
            return Task.CompletedTask;
        }

        private static bool IsReplayRequest(string nonce, long timestamp)
        {
            if (MemoryCache.Default.Contains(nonce))
                return true;

            long unix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            if (Math.Abs(timestamp - unix) > RequestTimeDelta)
                return true;

            MemoryCache.Default.Add(nonce, timestamp, DateTimeOffset.UtcNow.AddSeconds(RequestTimeDelta));
            return false;
        }
    }
}