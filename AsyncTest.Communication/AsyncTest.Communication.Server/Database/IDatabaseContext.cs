using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AsyncTest.Communication.Server.Database.Queue.QueueItem;

namespace AsyncTest.Communication.Server.Database
{
    public interface IDatabaseContext : IDisposable
    {
        DbSet<QueueItemEntity> QueueItems { get; }

        Task<int> SaveChangesAsync();
        DbSet<TEntity> Set<TEntity>() 
            where TEntity : class;
    }
}