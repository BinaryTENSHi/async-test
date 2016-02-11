using AsyncTest.Communication.Server.Database.Mapper;
using AsyncTest.Communication.Server.Database.Queue.QueueItem.MessageQueueItem;
using Ninject.Modules;

namespace AsyncTest.Communication.Server.Database.Queue.QueueItem
{
    public class QueueItemModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IQueueItemRepository>().To<QueueItemRepository>();
            Bind<IMapper, IMapper<QueueItemEntity, QueueItemDto>>().To<QueueItemMapper>();

            Bind<IMessageQueueItemRepository>().To<MessageQueueItemRepository>();
            Bind<IMapper, IMapper<MessageQueueItemEntity, MessageQueueItemDto>>().To<MessageQueueItemMapper>();
        }
    }
}