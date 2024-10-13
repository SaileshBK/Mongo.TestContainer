using Microsoft.Extensions.DependencyInjection;
using Mongo.TestContainer.Models.Constants;
using Mongo.TestContainer.Services.Interfaces;
using MongoDB.Driver;

namespace Mongo.TestContainer.Services.Interfaces;

internal sealed class MongoDbService([FromKeyedServices(TestContainerKeys.MongoTestContainerClientKey)] IMongoClient mongoClient) : IMongoDbService
{
    private readonly IMongoClient _mongoClient = mongoClient;

    public IMongoDatabase GetDatabase(string dataBaseName)
    {
        return _mongoClient.GetDatabase(dataBaseName);
    }
}
