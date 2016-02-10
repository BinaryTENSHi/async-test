using AsyncTest.Database.Database.Dto;
using AsyncTest.Database.Database.Mapper;
using AsyncTest.Database.Database.Model;

namespace AsyncTest.Database.Database.Repository
{
    public class BalloonRepository : AbstractRepository<BalloonEntity, BalloonDto>, IBalloonRepository
    {
        public BalloonRepository(IDatabaseContext databaseContext, IMapper<BalloonEntity, BalloonDto> mapper)
            : base(databaseContext, mapper)
        {
        }
    }
}