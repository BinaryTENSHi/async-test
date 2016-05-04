using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AsyncTest.Communication.Interface;
using AsyncTest.Communication.Interface.Queue;
using AsyncTest.Communication.Server.Database.Queue.QueueItem;
using AsyncTest.Communication.Server.Server.Authentication;

namespace AsyncTest.Communication.Server.Server.Controller
{
    public class QueueRestController : ApiController
    {
        private readonly IQueueItemRepository _queueItemRepository;

        public QueueRestController(IQueueItemRepository queueItemRepository)
        {
            _queueItemRepository = queueItemRepository;
        }

        [HttpGet]
        [Route(RestRoutes.QueueUrl)]
        [AuthenticationRequired]
        public async Task<QueueRest> GetAsync(HttpRequestMessage request)
        {
            IEnumerable<QueueItemDto> items = await _queueItemRepository.AllAsync().ConfigureAwait(false);
            QueueRest queue = new QueueRest();

            foreach (QueueItemDto item in items)
                queue.Items.Add(RestRoutes.MakeQueueItemLink(item.ItemType, item.Id));

            return queue;
        }
    }
}