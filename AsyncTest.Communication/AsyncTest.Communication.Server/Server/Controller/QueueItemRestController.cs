using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AsyncTest.Communication.Interface;
using AsyncTest.Communication.Interface.Queue;
using AsyncTest.Communication.Server.Database.Queue.QueueItem;

namespace AsyncTest.Communication.Server.Server.Controller
{
    public class QueueItemRestController : ApiController
    {
        private readonly IQueueItemRepository _queueItemRepository;

        public QueueItemRestController(IQueueItemRepository queueItemRepository)
        {
            _queueItemRepository = queueItemRepository;
        }

        [HttpGet]
        [Route(RestRoutes.QueueItemUrl)]
        [ResponseType(typeof(QueueItemRest))]
        public async Task<HttpResponseMessage> GetAsync(HttpRequestMessage request, Guid id)
        {
            QueueItemDto item = await _queueItemRepository.FindAsync(id).ConfigureAwait(false);
            if (item == null)
                return request.CreateErrorResponse(HttpStatusCode.NotFound, $"Could not find item with id '{id}'");

            return request.CreateResponse(HttpStatusCode.OK, item);
        }

        [HttpDelete]
        [Route(RestRoutes.QueueItemUrl)]
        public async Task<HttpResponseMessage> DeleteAsync(HttpRequestMessage request, Guid id)
        {
            bool success = await _queueItemRepository.DeleteAsync(id).ConfigureAwait(false);
            if (!success)
                return request.CreateErrorResponse(HttpStatusCode.NotFound, $"Could not delete item with id '{id}'");

            return request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}