using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AsyncTest.Communication.Interface;

namespace AsyncTest.Communication.Server.Server.Controller
{
    public class VersionRestController : ApiController
    {
        [HttpGet]
        [Route(RestRoutes.VersionUrl)]
        public Task<VersionRest> GetAsync(HttpRequestMessage request)
        {
            return Task.FromResult(new VersionRest());
        }
    }
}