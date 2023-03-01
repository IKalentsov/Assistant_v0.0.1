using Assistant.Application.Common.Interfaces;
using Assistant.Infrastructure.Persistence;
using Assistant.Infrastructure.Services;
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
            services.AddPooledDbContextFactory<DatabaseContext>
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

            services.AddScoped<IDatabaseContextFactory, DatabaseContextFactory>();
            services.AddScoped<IDatabaseContext>(provider => provider.GetRequiredService<IDatabaseContextFactory>().CreateContext());

            return services;
        }
    }
}
