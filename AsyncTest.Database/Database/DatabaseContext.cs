using System.Data.Entity;
using AsyncTest.Database.Database.Model;

namespace AsyncTest.Database.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<BalloonEntity> Balloons { get; set; }
    }
}