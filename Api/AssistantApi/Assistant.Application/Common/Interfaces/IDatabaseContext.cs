using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Assistant.Application.Common.Interfaces
{
    public interface IDatabaseContext
    {
        public DatabaseFacade Database { get; }
        public ChangeTracker ChangeTracker { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        public DbSet<TEntity> Set<TEntity>()
            where TEntity : class;
    }
}
