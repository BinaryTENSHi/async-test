using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using AsyncTest.Database.Database.Model;

namespace AsyncTest.Database.Database
{
    [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<BalloonEntity> Balloons { get; set; }
    }
}