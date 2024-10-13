using Microsoft.AspNetCore.Mvc;
using Mongo.TestContainer.Services.Interfaces;
using MongoDB.Bson;

namespace Mongo.TestContainer.Controllers;

public sealed class UserController(IMongoRepository<BsonDocument> repository) : ControllerBase
{
    private readonly IMongoRepository<BsonDocument> _repository = repository;

    [HttpGet]
    [Route("api/user-list")]
    public async Task<IActionResult> Get()
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

        foreach (var item in seedData)
        {
            await _repository.AddAsync(item);
        }

        var results = await _repository.GetAllAsync();
        return Ok(results.ToJson());
    }
}
