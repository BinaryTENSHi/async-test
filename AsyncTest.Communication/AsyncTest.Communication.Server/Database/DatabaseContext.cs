using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using AsyncTest.Communication.Server.Database.Queue.QueueItem;

namespace AsyncTest.Communication.Server.Database
{
    [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<QueueItemEntity> QueueItems { get; set; }
    }
}