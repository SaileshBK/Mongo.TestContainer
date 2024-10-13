using Microsoft.AspNetCore.Mvc;
using Mongo.TestContainer.Models.Constants;
using Mongo.TestContainer.Models.Database;
using Mongo.TestContainer.Services.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Mongo.TestContainer.Controllers;

public sealed class UserController(IMongoDbService mongoDbService) : ControllerBase
{
    private readonly IMongoDbService _mongoDbService = mongoDbService;

    [HttpGet]
    [Route("api/user-list")]
    public async Task<IActionResult> Get()
    {
        var database = _mongoDbService.GetDatabase(TestContainerKeys.TestContainerDatabase);
        var collection = database.GetCollection<BsonDocument>(nameof(UserProfile));

        // Create
        await collection.InsertOneAsync(new BsonDocument()
        {
            ["FirstName"] = "John",
            ["LastName"] = "Doe"
        });

        // Read
        var filterBuilder = Builders<BsonDocument>.Filter;
        var filter = filterBuilder.Eq("FirstName", "John");
        var results = collection.Find(filter).FirstOrDefault();

        return Ok(results.ToJson());
    }
}
