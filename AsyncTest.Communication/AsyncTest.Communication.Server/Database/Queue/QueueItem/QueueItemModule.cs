using Ninject.Modules;

namespace AsyncTest.Communication.Server.Database.Queue.QueueItem
{
    public class QueueItemModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IQueueItemRepository>().To<QueueItemRepository>();
            Bind<IMapper<QueueItemEntity, QueueItemDto>>().To<QueueItemMapper>();
        }
    }
}