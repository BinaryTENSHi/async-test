namespace AsyncTest.Communication.Server.Database.Queue.QueueItem
{
    public class QueueItemMapper : IMapper<QueueItemEntity, QueueItemDto>
    {
        public void MapToDto(QueueItemEntity source, QueueItemDto target)
        {
            target.Id = source.Id;
            target.ItemType = source.ItemType;
        }

        public void MapToEntity(QueueItemDto source, QueueItemEntity target)
        {
            target.Id = source.Id;
            target.ItemType = source.ItemType;
        }
    }
}