using MongoDB.Driver;

namespace Mongo.TestContainer.Services.Interfaces;

public interface IMongoDbService
{
    IMongoDatabase GetDatabase(string dataBaseName);
}
