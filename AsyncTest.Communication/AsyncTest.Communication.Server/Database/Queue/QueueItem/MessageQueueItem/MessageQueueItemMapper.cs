using AsyncTest.Communication.Server.Database.Mapper;

namespace AsyncTest.Communication.Server.Database.Queue.QueueItem.MessageQueueItem
{
    public class MessageQueueItemMapper : AbstractMapper<MessageQueueItemEntity, MessageQueueItemDto>
    {
        public override MessageQueueItemDto CreateDto(MessageQueueItemEntity source)
        {
            return new MessageQueueItemDto
            {
                Id = source.Id,
                ItemType = source.ItemType,
                Message = source.Message
            };
        }

        public override void MapToEntity(MessageQueueItemDto source, MessageQueueItemEntity target)
        {
            target.Id = source.Id;
            target.ItemType = source.ItemType;
            target.Message = source.Message;
        }
    }
}