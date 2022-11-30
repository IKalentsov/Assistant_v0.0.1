using Assistant.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assistant.Infrastructure.Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }
    }
}
