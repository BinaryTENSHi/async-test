using AsyncTest.Communication.Client.Communication.Queue.QueueItem;

namespace AsyncTest.Communication.Client.Communication.Queue
{
    public interface IQueueItemHandlerDictionary
    {
        IQueueItemHandler GetHandler(string relation);
    }
}