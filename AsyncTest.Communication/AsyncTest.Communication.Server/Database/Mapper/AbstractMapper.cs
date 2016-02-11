namespace AsyncTest.Communication.Server.Database.Mapper
{
    public abstract class AbstractMapper<TEntity, TDto> : IMapper<TEntity, TDto>
    {
        public dynamic CreateDto(dynamic source)
        {
            return CreateDto((TEntity) source);
        }

        public void MapToEntity(dynamic source, dynamic target)
        {
            MapToEntity((TDto) source, (TEntity) target);
        }

        public abstract TDto CreateDto(TEntity source);
        public abstract void MapToEntity(TDto source, TEntity target);
    }
}