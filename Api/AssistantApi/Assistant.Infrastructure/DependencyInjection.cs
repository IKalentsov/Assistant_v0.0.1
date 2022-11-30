using Assistant.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Assistant.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultDatabaseConnection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DatabaseContext>
            (
                builder =>
                {
                    builder.UseNpgsql
                    (
                        defaultDatabaseConnection, b =>
                            b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)
                    );
                }
            );

            return services;
        }
    }
}
