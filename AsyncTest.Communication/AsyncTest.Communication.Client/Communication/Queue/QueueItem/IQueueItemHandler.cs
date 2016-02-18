using System;
using System.Threading.Tasks;

namespace AsyncTest.Communication.Client.Communication.Queue.QueueItem
{
    public interface IQueueItemHandler
    {
        string Relation { get; }
        Task HandleAsync(Uri href);
    }
}