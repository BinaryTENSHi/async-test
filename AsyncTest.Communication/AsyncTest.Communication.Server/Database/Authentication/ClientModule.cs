using AsyncTest.Communication.Server.Database.Mapper;
using Ninject.Modules;

namespace AsyncTest.Communication.Server.Database.Authentication
{
    public class ClientModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IClientRepository>().To<ClientRepository>();
            Bind<IMapper, IMapper<ClientEntity, ClientDto>>().To<ClientMapper>();
        }
    }
}