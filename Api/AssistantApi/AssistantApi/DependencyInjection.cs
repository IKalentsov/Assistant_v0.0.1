namespace AssistantApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
