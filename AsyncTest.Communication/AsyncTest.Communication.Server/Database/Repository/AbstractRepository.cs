using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncTest.Communication.Server.Database.Repository
{
    public abstract class AbstractRepository<TEntity, TDto> : IRepository<TDto>
        where TEntity : class, IEntity, new()
        where TDto : class, IDto, new()
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IMapper<TEntity, TDto> _mapper;

        protected AbstractRepository(IDatabaseContext databaseContext, IMapper<TEntity, TDto> mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public Task<bool> AnyAsync()
        {
            return _databaseContext.Set<TEntity>().AnyAsync();
        }

        public async Task<IEnumerable<TDto>> AllAsync()
        {
            IList<TEntity> data = await _databaseContext.Set<TEntity>().ToListAsync().ConfigureAwait(false);
            return data.Select(ToData);
        }

        public async Task<TDto> FindAsync(Guid id)
        {
            TEntity entity = await _databaseContext.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
            if (entity == null)
                return null;

            return ToData(entity);
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

        public async Task<bool> DeleteAsync(Guid id)
        {
            TEntity entity = await _databaseContext.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
            if (entity == null)
                return false;

            _databaseContext.Set<TEntity>().Remove(entity);
            int rows = await _databaseContext.SaveChangesAsync().ConfigureAwait(false);
            return rows > 0;
        }

        public Task SaveChangesAsync()
        {
            return _databaseContext.SaveChangesAsync();
        }

        private TDto ToData(TEntity entity)
        {
            TDto dto = new TDto();
            _mapper.MapToDto(entity, dto);
            return dto;
        }
    }
}