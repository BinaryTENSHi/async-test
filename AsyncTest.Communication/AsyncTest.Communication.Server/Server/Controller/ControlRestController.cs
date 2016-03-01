using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AsyncTest.Communication.Interface;
using AsyncTest.Communication.Interface.Control;
using AsyncTest.Communication.Server.Service;

namespace AsyncTest.Communication.Server.Server.Controller
{
    public class ControlRestController : ApiController
    {
        private readonly IControlService _controlService;

        public ControlRestController(IControlService controlService)
        {
            _controlService = controlService;
        }

        [HttpGet]
        [Route(RestRoutes.ControlUrl)]
        [ResponseType(typeof(ControlRest))]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            ControlRest controlRest = new ControlRest {ShouldPollQueue = _controlService.ShouldPoll};
            return request.CreateResponse(controlRest);
        }
    }
}