using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AsyncTest.Communication.Interface;
using AsyncTest.Communication.Server.Server.Authentication;

namespace AsyncTest.Communication.Server.Server.Controller
{
    public class VersionRestController : ApiController
    {
        [HttpGet]
        [Route(RestRoutes.VersionUrl)]
        [AuthenticationRequired]
        public Task<VersionRest> GetAsync(HttpRequestMessage request)
        {
            return Task.FromResult(new VersionRest());
        }
    }
}