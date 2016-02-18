using AsyncTest.Communication.Client.Communication;
using AsyncTest.Communication.Client.Communication.Queue;
using AsyncTest.Communication.Client.Communication.Queue.QueueItem;
using Caliburn.Micro;
using Ninject.Modules;

namespace AsyncTest.Communication.Client
{
    public class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWindowManager>().To<WindowManager>();

            Bind<IMainViewModel>().To<MainViewModel>();

            Bind<IRestClient>().To<RestClient>();
            Bind<IQueuePoller>().To<QueuePoller>();
            Bind<IQueueItemHandlerDictionary>().To<QueueItemHandlerDictionary>();

            Bind<IQueueItemHandler>().To<MessageQueueItemHandler>();
        }
    }
}