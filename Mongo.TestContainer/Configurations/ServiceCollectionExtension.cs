using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Mongo.TestContainer.Models.Constants;
using Mongo.TestContainer.Repository;
using Mongo.TestContainer.Services.Interfaces;
using Mongo.TestContainer.Services.Utilities;
using MongoDB.Driver;
using Testcontainers.MongoDb;

namespace Mongo.TestContainer.Configurations;

internal static class ServiceCollectionExtension
{
    public static async Task ConfigureServices(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddControllers();
        services.AddSwaggerServices();
        await services.RegisterServices(configuration);
    }

    private static async Task RegisterServices(this IServiceCollection services, IConfigurationManager configuration)
    {
        await services.RegisterMongoDbServices(configuration);
        services.TryAddSingleton<IMongoDbService, MongoDbService>();
    }

    private static async Task RegisterMongoDbServices(this IServiceCollection services, IConfigurationManager configuration)
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
            .WithUsername(RandomUtility.GenerateRandomString(7))
            .WithPassword(RandomUtility.GenerateRandomString(10))
            .Build();
        await container.StartAsync();
        return container;
    }

    private static void AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Mongo.TestContainer API",
                Description = "A Web API project using Mongo TestContainer.",
                Contact = new OpenApiContact
                {
                    Name = "Github",
                    Url = new Uri("https://github.com/SaileshBK/Mongo.TestContainer")
                }
            });
        });
    }
}
