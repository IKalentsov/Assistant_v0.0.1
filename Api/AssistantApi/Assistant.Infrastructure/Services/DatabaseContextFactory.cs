using Assistant.Application.Common.Interfaces;
using Assistant.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Assistant.Infrastructure.Services
{
    public class DatabaseContextFactory : IDatabaseContextFactory
    {
        private readonly IDbContextFactory<DatabaseContext> _contextFactory;

        public DatabaseContextFactory(IDbContextFactory<DatabaseContext> pooledFactory)
        {
            _contextFactory = pooledFactory;
        }

        public async Task<IDatabaseContext> CreateContextAsync(CancellationToken cancellationToken = default)
        {
            return await _contextFactory.CreateDbContextAsync(cancellationToken);
        }

        public IDatabaseContext CreateContext()
        {
            return _contextFactory.CreateDbContext();
        }
    }
}
