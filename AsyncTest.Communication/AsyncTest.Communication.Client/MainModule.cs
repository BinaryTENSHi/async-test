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
        }
    }
}