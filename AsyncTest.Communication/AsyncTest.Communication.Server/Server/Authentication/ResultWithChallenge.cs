using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using AsyncTest.Communication.Interface.Authentication;

namespace AsyncTest.Communication.Server.Server.Authentication
{
    public class ResultWithChallenge : IHttpActionResult
    {
        private readonly IHttpActionResult _next;

        public ResultWithChallenge(IHttpActionResult next)
        {
            _next = next;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await _next.ExecuteAsync(cancellationToken).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(AuthenticationValues.AuthenticationScheme));

            return response;
        }
    }
}