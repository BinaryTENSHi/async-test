using AsyncTest.Communication.Client.Communication;
using AsyncTest.Communication.Client.Communication.Control;
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
            Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();

            Bind<IMainViewModel>().To<MainViewModel>();

            Bind<IRestClient>().To<RestClient>();
            Bind<IQueuePoller>().To<QueuePoller>().InSingletonScope();
            Bind<IControlPoller>().To<ControlPoller>().InSingletonScope();
            Bind<IAuthorizationContainer>().To<AuthorizationContainer>().InSingletonScope();
            Bind<IQueueItemHandlerDictionary>().To<QueueItemHandlerDictionary>();

            Bind<IQueueItemHandler>().To<MessageQueueItemHandler>();
        }
    }
}