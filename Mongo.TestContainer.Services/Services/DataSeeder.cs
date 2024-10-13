using Mongo.TestContainer.Models.Constants;
using Mongo.TestContainer.Models.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Mongo.TestContainer.Services.Interfaces;

internal sealed class DataSeeder(IMongoDbService mongoDbService) : IDataSeeder
{
    private readonly IMongoDbService _mongoDbService = mongoDbService;

    public async Task SeedDataAsync()
    {
        var database = _mongoDbService.GetDatabase(TestContainerKeys.TestContainerDatabase);
        var collection = database.GetCollection<BsonDocument>(nameof(UserProfile));
        var existingUserProfile = await collection.CountDocumentsAsync(Builders<BsonDocument>.Filter.Empty);

        if (existingUserProfile == 0)
        {
            var seedData = new List<BsonDocument>() 
            {
                new()
                {
                    ["FirstName"] = "John",
                    ["LastName"] = "Doe"
                },
                new()
                {
                    ["FirstName"] = "Jane",
                    ["LastName"] = "Doe"
                },
                new()
                {
                    ["FirstName"] = "Michael",
                    ["LastName"] = "Doe"
                }
            };
            await collection.InsertManyAsync(seedData);
        }
    }
}
