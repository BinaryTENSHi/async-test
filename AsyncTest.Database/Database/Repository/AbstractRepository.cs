using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AsyncTest.Database.Database.Dto;
using AsyncTest.Database.Database.Mapper;

namespace AsyncTest.Database.Database.Repository
{
    public abstract class AbstractRepository<TEntity, TDto> : IRepository<TEntity, TDto>
        where TEntity : class, new()
        where TDto : IDto, new()
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IMapper<TEntity, TDto> _mapper;

        protected AbstractRepository(IDatabaseContext databaseContext, IMapper<TEntity, TDto> mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        protected DbSet<TEntity> Set => _databaseContext.Set<TEntity>();

        public Task<bool> AnyAsync()
        {
            return _databaseContext.Set<TEntity>().AnyAsync();
        }

        public async Task<IEnumerable<TDto>> AllAsync()
        {
            IList<TEntity> data = await _databaseContext.Set<TEntity>().ToListAsync().ConfigureAwait(false);
            return data.Select(ToData);
        }


        public void Insert(TDto dto)
        {
            TEntity entity = _databaseContext.Set<TEntity>().Create();
            _mapper.MapToEntity(dto, entity);
            _databaseContext.Set<TEntity>().Add(entity);
        }

        public async Task UpdateAsync(TDto dto)
        {
            TEntity entity = await _databaseContext.Set<TEntity>().FindAsync(dto.Id).ConfigureAwait(false);
            _mapper.MapToEntity(dto, entity);
        }

        public Task SaveChangesAsync()
        {
            return _databaseContext.SaveChangesAsync();
        }

        protected TDto ToData(TEntity entity)
        {
            TDto dto = new TDto();
            _mapper.MapToDto(entity, dto);
            return dto;
        }
    }
}