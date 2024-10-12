namespace Mongo.TestContainer.Configurations;

internal static class ServiceCollectionExtension
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        RegisterControllers(services);
    }

    private static void RegisterControllers(IServiceCollection services)
    {
        services.AddControllers();
    }
}
