using System.Collections.Generic;
using AsyncTest.Communication.Client.Communication.Queue.QueueItem;

namespace AsyncTest.Communication.Client.Communication.Queue
{
    public class QueueItemHandlerDictionary : IQueueItemHandlerDictionary
    {
        private readonly IDictionary<string, IQueueItemHandler> _queueItemHandlerDictionary =
            new Dictionary<string, IQueueItemHandler>();

        public QueueItemHandlerDictionary(IEnumerable<IQueueItemHandler> queueItemHandlers)
        {
            foreach (IQueueItemHandler queueItemHandler in queueItemHandlers)
            {
                _queueItemHandlerDictionary.Add(queueItemHandler.Relation, queueItemHandler);
            }
        }

        public IQueueItemHandler GetHandler(string relation)
        {
            return _queueItemHandlerDictionary[relation];
        }
    }
}