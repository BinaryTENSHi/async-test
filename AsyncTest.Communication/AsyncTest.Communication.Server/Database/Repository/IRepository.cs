using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncTest.Communication.Server.Database.Repository
{
    public interface IRepository<TDto>
        where TDto : IDto
    {
        Task<bool> AnyAsync();
        Task<IEnumerable<TDto>> AllAsync();
        Task<TDto> FindAsync(Guid id);
        void Insert(TDto dto);
        Task UpdateAsync(TDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task SaveChangesAsync();
    }
}