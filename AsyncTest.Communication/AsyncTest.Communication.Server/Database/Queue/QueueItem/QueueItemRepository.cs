using AsyncTest.Communication.Server.Database.Mapper;
using AsyncTest.Communication.Server.Database.Repository;

namespace AsyncTest.Communication.Server.Database.Queue.QueueItem
{
    public class QueueItemRepository : AbstractRepository<QueueItemEntity, QueueItemDto>, IQueueItemRepository
    {
        public QueueItemRepository(IDatabaseContext databaseContext, IMapperDictionary mapperDictionary)
            : base(databaseContext, mapperDictionary)
        {
        }
    }
}