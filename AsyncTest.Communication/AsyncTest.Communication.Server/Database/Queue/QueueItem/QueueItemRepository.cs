using AsyncTest.Communication.Server.Database.Mapper;
using AsyncTest.Communication.Server.Database.Repository;
using Caliburn.Micro;

namespace AsyncTest.Communication.Server.Database.Queue.QueueItem
{
    public class QueueItemRepository : AbstractRepository<QueueItemEntity, QueueItemDto>, IQueueItemRepository
    {
        public QueueItemRepository(IDatabaseContext databaseContext, IMapperDictionary mapperDictionary, IEventAggregator eventAggregator)
            : base(databaseContext, mapperDictionary, eventAggregator)
        {
        }
    }
}