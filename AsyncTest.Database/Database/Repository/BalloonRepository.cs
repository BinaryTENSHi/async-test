using System.Data.Entity;
using System.Threading.Tasks;
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

        public async Task<BalloonDto> GetByColorAsync(BalloonColor color)
        {
            BalloonEntity entity = await Set.SingleOrDefaultAsync(x => x.Color == color).ConfigureAwait(false);
            return ToData(entity);
        }
    }
}