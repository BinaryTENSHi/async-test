using AsyncTest.Communication.Server.Database.Mapper;

namespace AsyncTest.Communication.Server.Database.Authentication
{
    public class ClientMapper : AbstractMapper<ClientEntity, ClientDto>
    {
        public override ClientDto CreateDto(ClientEntity source)
        {
            return new ClientDto
            {
                Id = source.Id,
                SharedSecret = source.SharedSecret
            };
        }

        public override void MapToEntity(ClientDto source, ClientEntity target)
        {
            target.Id = source.Id;
            target.SharedSecret = source.SharedSecret;
        }
    }
}