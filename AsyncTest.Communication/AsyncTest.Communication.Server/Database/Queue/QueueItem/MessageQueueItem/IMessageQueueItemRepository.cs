using AsyncTest.Communication.Server.Database.Repository;

namespace AsyncTest.Communication.Server.Database.Queue.QueueItem.MessageQueueItem
{
    public interface IMessageQueueItemRepository : IRepository<MessageQueueItemDto>
    {
    }
}