namespace AsyncTest.Communication.Server.Database.Mapper
{
    public interface IMapper
    {
        dynamic CreateDto(dynamic source);
        void MapToEntity(dynamic source, dynamic target);
    }

    public interface IMapper<in TEntity, TDto> : IMapper
    {
        TDto CreateDto(TEntity source);
        void MapToEntity(TDto source, TEntity target);
    }
}