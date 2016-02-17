using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AsyncTest.Communication.Interface;
using AsyncTest.Communication.Interface.Queue;

namespace AsyncTest.Communication.Client.Communication
{
    public class QueuePoller : AbstractAsyncPoller, IQueuePoller
    {
        private readonly Uri _baseUri = new Uri("http://localhost:6688/");
        private readonly IRestClient _restClient;

        public QueuePoller(IRestClient restClient)
        {
            _restClient = restClient;
            SetInterval(TimeSpan.FromSeconds(1));
        }

        public override async Task PollAsync()
        {
            QueueRest queue = await _restClient.GetAsync<QueueRest>(new Uri(_baseUri, "/queue/")).ConfigureAwait(false);
            foreach (LinkRest linkRest in queue.Items)
            {
                QueueItemRest queueItem = await _restClient.GetAsync<QueueItemRest>(new Uri(_baseUri, linkRest.Href)).ConfigureAwait(false);
                Debug.WriteLine($"Received QueueItem: {queueItem.Id} of type {queueItem.ItemType}");
                await _restClient.DeleteAsync(new Uri(_baseUri, linkRest.Href)).ConfigureAwait(false);
            }
        }
    }
}