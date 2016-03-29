using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public async Task<IEnumerable<BalloonDto>> GetByColorAsync(BalloonColor color)
        {
            IList<BalloonEntity> entities = await Set.Where(x => x.Color == color).ToListAsync().ConfigureAwait(false);
            return entities.Select(ToData);
        }
    }
}