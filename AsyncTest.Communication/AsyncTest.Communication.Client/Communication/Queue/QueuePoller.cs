using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AsyncTest.Communication.Client.Communication.Queue.QueueItem;
using AsyncTest.Communication.Interface;
using AsyncTest.Communication.Interface.Queue;

namespace AsyncTest.Communication.Client.Communication.Queue
{
    public class QueuePoller : AbstractAsyncPoller, IQueuePoller
    {
        private readonly Uri _baseUri = new Uri("http://localhost:6688/");
        private readonly IQueueItemHandlerDictionary _queueItemHandlerDictionary;
        private readonly IRestClient _restClient;

        public QueuePoller(IRestClient restClient, IQueueItemHandlerDictionary queueItemHandlerDictionary)
        {
            _restClient = restClient;
            _queueItemHandlerDictionary = queueItemHandlerDictionary;
            SetInterval(TimeSpan.FromSeconds(1));
        }

        public override async Task PollAsync()
        {
            QueueRest queue = await _restClient.GetAsync<QueueRest>(new Uri(_baseUri, "/queue/")).ConfigureAwait(false);
            foreach (LinkRest linkRest in queue.Items)
            {
                IQueueItemHandler handler = _queueItemHandlerDictionary.GetHandler(linkRest.Relation);
                Uri itemUri = new Uri(_baseUri, linkRest.Href);
                await handler.HandleAsync(itemUri).ConfigureAwait(false);
                await _restClient.DeleteAsync(itemUri).ConfigureAwait(false);
            }
        }

        protected override void OnException(Exception exception)
        {
            Debug.WriteLine($"Exception Occurred: ${exception.GetType()}\n{exception}");
        }
    }
}