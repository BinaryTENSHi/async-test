using AsyncTest.Communication.Server.Database;
using AsyncTest.Communication.Server.Database.Mapper;
using AsyncTest.Communication.Server.Server;
using Caliburn.Micro;
using Ninject.Modules;

namespace AsyncTest.Communication.Server
{
    public class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWindowManager>().To<WindowManager>();
            Bind<IMainViewModel>().To<MainViewModel>();
            Bind<IRestServer>().To<RestServer>();
            Bind<IDatabaseContext>().To<DatabaseContext>();
            Bind<IMapperDictionary>().To<MapperDictionary>().InSingletonScope();
        }
    }
}