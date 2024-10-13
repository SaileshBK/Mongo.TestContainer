using Mongo.TestContainer.Models.Constants;
using Mongo.TestContainer.Repository;
using Mongo.TestContainer.Services.Interfaces;
using Mongo.TestContainer.Services.Services;
using MongoDB.Driver;
using Testcontainers.MongoDb;

namespace Mongo.TestContainer.Configurations;

internal static class ServiceCollectionExtension
{
    public static async Task ConfigureServices(this IServiceCollection services)
    {
        services.AddControllers();
        await RegisterServices(services);
    }

    private static async Task RegisterServices(IServiceCollection services)
    {
        await RegisterMongoDbServices(services);
        services.AddSingleton<IMongoDbService, MongoDbService>();
    }

    private static async Task RegisterMongoDbServices(IServiceCollection services)
    {
        var mongoContainer = await StartMongoDbCoontainer();
        services.AddKeyedSingleton(mongoContainer, "MongoDbContainer");
        services.AddKeyedSingleton<IMongoClient, MongoClient>(TestContainerKeys.MongoTestContainerClientKey, (sp, key) =>
        {
            return key switch
            {
                TestContainerKeys.MongoTestContainerClientKey => new MongoClient(mongoContainer.GetConnectionString()),
                _ => throw new InvalidOperationException(ExceptionMessages.InvalidServiceKey)
            };
        });
        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
    }

    private static async Task<MongoDbContainer> StartMongoDbCoontainer()
    {
        var container = new MongoDbBuilder()
            .WithImage("mongo:latest")
            .Build();
        await container.StartAsync();
        return container;
    }
}
