using AsyncTest.Communication.Server.Database.Mapper;
using AsyncTest.Communication.Server.Database.Repository;
using Caliburn.Micro;

namespace AsyncTest.Communication.Server.Database.Authentication
{
    public class ClientRepository : AbstractRepository<ClientEntity, ClientDto>, IClientRepository
    {
        public ClientRepository(
            IDatabaseContext databaseContext,
            IMapperDictionary mapperDictionary,
            IEventAggregator eventAggregator)
            : base(databaseContext, mapperDictionary, eventAggregator)
        {
        }
    }
}