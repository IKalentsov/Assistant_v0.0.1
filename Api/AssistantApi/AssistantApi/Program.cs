using Assistant.Application;
using Assistant.Application.Common.Interfaces;
using Assistant.Domain.Models;
using Assistant.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace AssistantApi;

public class Program
{
    private static ConfigurationManager _configuration;

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        BuildConfiguration(builder.Configuration);
        ConfigureServices(builder.Services);

        var app = builder.Build();
        ConfigureApplication(app);

        MigrateDatabase(app);

        app.Run();
    }

    private static void MigrateDatabase(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var factory = services.GetRequiredService<IDatabaseContextFactory>();

            var context = factory.CreateContext();

            context.Database.Migrate();
        }
    }

    private static void ConfigureApplication(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Assistant v1"));
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseRouting();

        app.MapControllers();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddApplication();
        services.AddConfigureSettings(_configuration);
        services.AddInfrastructure(_configuration);
        services.AddOptions();

        services.AddAuthorization();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // указывает, будет ли валидироваться издатель при валидации токена
                    ValidateIssuer = true,
                    // строка, представляющая издателя
                    ValidIssuer = "MyAuthServer",
                    // будет ли валидироваться потребитель токена
                    ValidateAudience = true,
                    // установка потребителя токена
                    ValidAudience = "MyAuthServer",
                    //будет ли валидироваться время существования
                    ValidateLifetime = true,
                    // установка ключа безопасности
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                        _configuration.GetSection(SecuritySettings.Path).Get<SecuritySettings>().JWTSecretKey)),
                    // валидация ключа безопасности
                    ValidateIssuerSigningKey = true,
                };
            });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            var securityDefinition = new OpenApiSecurityScheme()
            {
                Name = "Bearer",
                BearerFormat = "JWT",
                Scheme = "bearer",
                Description = "Specify the authorization token.",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
            };
            c.AddSecurityDefinition("jwt_auth", securityDefinition);

            // Make sure swagger UI requires a Bearer token specified
            var securityScheme = new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Id = "jwt_auth",
                    Type = ReferenceType.SecurityScheme
                }
            };
            var securityRequirements = new OpenApiSecurityRequirement()
            {
                {securityScheme, Array.Empty<string>()},
            };
                    c.AddSecurityRequirement(securityRequirements);
         });
    }

    private static void BuildConfiguration(ConfigurationManager configuration)
    {
        configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile
            (
                "appsettings.json",
                optional: false,
                reloadOnChange: true
            )
            .AddJsonFile
            (
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                optional: true,
                reloadOnChange: true
        )
        .Build();

        _configuration = configuration;
    }
}
