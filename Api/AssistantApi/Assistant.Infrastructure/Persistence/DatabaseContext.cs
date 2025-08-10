using Assistant.Application.Common.Interfaces;
using Assistant.Domain.Entities;
using Assistant.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Assistant.Infrastructure.Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.UserConfiguration());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateDateTime();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateDateTime()
        {
            var dateTime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

            foreach (var entry in ChangeTracker.Entries<IAuditable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = dateTime;
                        entry.Entity.LastModifiedOn = dateTime;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = dateTime;
                        break;
                }
            }
        }
    }
}
