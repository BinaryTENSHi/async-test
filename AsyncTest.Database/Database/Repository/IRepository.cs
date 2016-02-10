using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncTest.Database.Database.Dto;

namespace AsyncTest.Database.Database.Repository
{
    public interface IRepository<TEntity, TDto>
        where TEntity : class
        where TDto : IDto
    {
        Task<bool> AnyAsync();
        Task<IEnumerable<TDto>> AllAsync();
        void Insert(TDto dto);
        Task UpdateAsync(TDto dto);
        Task SaveChangesAsync();
    }
}