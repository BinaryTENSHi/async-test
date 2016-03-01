using AsyncTest.Communication.Server.Database.Mapper;
using AsyncTest.Communication.Server.Database.Repository;
using Caliburn.Micro;

namespace AsyncTest.Communication.Server.Database.Queue.QueueItem.MessageQueueItem
{
    public class MessageQueueItemRepository : AbstractRepository<MessageQueueItemEntity, MessageQueueItemDto>,
        IMessageQueueItemRepository
    {
        public MessageQueueItemRepository(IDatabaseContext databaseContext, IMapperDictionary mapperDictionary,
            IEventAggregator eventAggregator)
            : base(databaseContext, mapperDictionary, eventAggregator)
        {
        }
    }
}