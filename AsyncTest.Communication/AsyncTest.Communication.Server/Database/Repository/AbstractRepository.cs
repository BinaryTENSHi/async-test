using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AsyncTest.Communication.Server.Database.Mapper;
using AsyncTest.Communication.Server.Event;
using Caliburn.Micro;

namespace AsyncTest.Communication.Server.Database.Repository
{
    public abstract class AbstractRepository<TEntity, TDto> : IRepository<TDto>
        where TEntity : class, IEntity, new()
        where TDto : class, IDto, new()
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMapperDictionary _mapperDictionary;

        protected AbstractRepository(IDatabaseContext databaseContext, IMapperDictionary mapperDictionary,
            IEventAggregator eventAggregator)
        {
            _databaseContext = databaseContext;
            _mapperDictionary = mapperDictionary;
            _eventAggregator = eventAggregator;
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
            _mapperDictionary.GetMapperForType(entity.GetType()).MapToEntity(dto, entity);
            _databaseContext.Set<TEntity>().Add(entity);
            _eventAggregator.PublishOnBackgroundThread(new EntityChangedEvent<TEntity>(entity.Id));
        }

        public async Task UpdateAsync(TDto dto)
        {
            TEntity entity = await _databaseContext.Set<TEntity>().FindAsync(dto.Id).ConfigureAwait(false);
            _mapperDictionary.GetMapperForType(entity.GetType()).MapToEntity(dto, entity);
            _eventAggregator.PublishOnBackgroundThread(new EntityChangedEvent<TEntity>(entity.Id));
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            TEntity entity = await _databaseContext.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
            if (entity == null)
                return false;

            _databaseContext.Set<TEntity>().Remove(entity);
            int rows = await _databaseContext.SaveChangesAsync().ConfigureAwait(false);
            _eventAggregator.PublishOnBackgroundThread(new EntityChangedEvent<TEntity>(id));
            return rows > 0;
        }

        public Task SaveChangesAsync()
        {
            return _databaseContext.SaveChangesAsync();
        }

        private TDto ToData(TEntity entity)
        {
            TDto dto = _mapperDictionary.GetMapperForType(entity.GetType()).CreateDto(entity);
            return dto;
        }
    }
}