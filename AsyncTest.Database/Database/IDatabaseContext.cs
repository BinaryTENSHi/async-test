using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AsyncTest.Database.Database.Model;

namespace AsyncTest.Database.Database
{
    public interface IDatabaseContext : IDisposable
    {
        DbSet<BalloonEntity> Balloons { get; }

        Task<int> SaveChangesAsync();

        DbSet<T> Set<T>()
            where T : class;
    }
}