using AsyncTest.Database.Database;
using AsyncTest.Database.Database.Dto;
using AsyncTest.Database.Database.Mapper;
using AsyncTest.Database.Database.Model;
using AsyncTest.Database.Database.Repository;
using Caliburn.Micro;
using Ninject.Modules;

namespace AsyncTest.Database
{
    public class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWindowManager>().To<WindowManager>();

            Bind<IMainViewModel>().To<MainViewModel>();

            Bind<IDatabaseContext>().To<DatabaseContext>();
            Bind<IBalloonRepository>().To<BalloonRepository>();
            Bind<IMapper<BalloonEntity, BalloonDto>>().To<BalloonMapper>();
        }
    }
}