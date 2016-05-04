using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using AsyncTest.Communication.Interface.Authentication;

namespace AsyncTest.Communication.Client.Communication
{
    public class HMAC256Handler : DelegatingHandler
    {
        private readonly IAuthorizationContainer _authorizationContainer;

        public HMAC256Handler(IAuthorizationContainer authorizationContainer)
        {
            _authorizationContainer = authorizationContainer;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            string nonce = Convert.ToBase64String(AuthenticationHelper.SecureBytes(16));
            string contentHash = await AuthenticationHelper.ComputeContentHashAsync(request.Content).ConfigureAwait(false);

            string signatureData = AuthenticationHelper.CreateSignatureData(_authorizationContainer.AppKey, request, timestamp, nonce, contentHash);
            string signature = AuthenticationHelper.CalculateHMACSHA256(_authorizationContainer.SharedSecret, signatureData);
            string authHeader = AuthenticationHelper.CreateAuthenticationHeader(_authorizationContainer.AppKey, signature, nonce, timestamp);
            request.Headers.Authorization = new AuthenticationHeaderValue(AuthenticationValues.AuthenticationScheme, authHeader);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}