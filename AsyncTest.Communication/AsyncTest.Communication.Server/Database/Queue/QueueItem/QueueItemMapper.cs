using AsyncTest.Communication.Server.Database.Mapper;

namespace AsyncTest.Communication.Server.Database.Queue.QueueItem
{
    public class QueueItemMapper : AbstractMapper<QueueItemEntity, QueueItemDto>
    {
        public override QueueItemDto CreateDto(QueueItemEntity source)
        {
            return new QueueItemDto
            {
                Id = source.Id,
                ItemType = source.ItemType
            };
        }

        public override void MapToEntity(QueueItemDto source, QueueItemEntity target)
        {
            target.Id = source.Id;
            target.ItemType = source.ItemType;
        }
    }
}