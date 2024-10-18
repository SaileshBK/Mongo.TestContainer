using Microsoft.Extensions.DependencyInjection.Extensions;
using Mongo.TestContainer.Models.Constants;
using Mongo.TestContainer.Repository;
using Mongo.TestContainer.Services.Interfaces;
using MongoDB.Driver;
using Testcontainers.MongoDb;

namespace Mongo.TestContainer.Configurations;

internal static class ServiceCollectionExtension
{
    public static async Task ConfigureServices(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddControllers();
        await RegisterServices(services, configuration);
    }

    private static async Task RegisterServices(IServiceCollection services, IConfigurationManager configuration)
    {
        await RegisterMongoDbServices(services, configuration);
        services.TryAddSingleton<IMongoDbService, MongoDbService>();
    }

    private static async Task RegisterMongoDbServices(IServiceCollection services, IConfigurationManager configuration)
    {
        var mongoContainer = await StartMongoDbContainer(configuration);
        services.AddKeyedSingleton(TestContainerKeys.MongoDbContainerInstanceKey, mongoContainer);
        services.AddKeyedSingleton<IMongoClient, MongoClient>(TestContainerKeys.MongoTestContainerClientKey, (sp, key) =>
        {
            return key switch
            {
                TestContainerKeys.MongoTestContainerClientKey => new MongoClient(mongoContainer.GetConnectionString()),
                _ => throw new InvalidOperationException(ExceptionMessages.InvalidServiceKey)
            };
        });
        services.TryAddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
        services.TryAddSingleton<IDataSeeder, DataSeeder>();
    }

    private static async Task<MongoDbContainer> StartMongoDbContainer(IConfigurationManager configuration)
    {
        var container = new MongoDbBuilder()
            .WithImage(configuration.GetValue<string>("MongoImage") ?? "mongo:latest")
            .Build();
        await container.StartAsync();
        return container;
    }
}
