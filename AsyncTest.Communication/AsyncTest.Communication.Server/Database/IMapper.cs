namespace AsyncTest.Communication.Server.Database
{
    public interface IMapper<in TEntity, in TDto>
    {
        void MapToDto(TEntity source, TDto target);
        void MapToEntity(TDto source, TEntity target);
    }
}