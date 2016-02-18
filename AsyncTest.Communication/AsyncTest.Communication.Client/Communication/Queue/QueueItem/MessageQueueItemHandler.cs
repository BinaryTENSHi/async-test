using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AsyncTest.Communication.Interface;
using AsyncTest.Communication.Interface.Queue;

namespace AsyncTest.Communication.Client.Communication.Queue.QueueItem
{
    public class MessageQueueItemHandler : IQueueItemHandler
    {
        private readonly IRestClient _restClient;

        public MessageQueueItemHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public string Relation => RestRelations.MessageQueueItemRelation;

        public async Task HandleAsync(Uri href)
        {
            MessageQueueItemRest queueItem =
                await _restClient.GetAsync<MessageQueueItemRest>(href).ConfigureAwait(false);
            Debug.WriteLine($"Received MessageQueueItem: {queueItem.Id}, {queueItem.Message}");
        }
    }
}