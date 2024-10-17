using Microsoft.AspNetCore.Mvc;
using Mongo.TestContainer.Services.Interfaces;
using MongoDB.Bson;

namespace Mongo.TestContainer.Controllers;

public sealed class UserController(
    IMongoRepository<BsonDocument> repository) : ControllerBase
{
    private readonly IMongoRepository<BsonDocument> _repository = repository;

    [HttpGet]
    [Route("api/user-list")]
    public async Task<IActionResult> Get()
    {
        var results = await _repository.GetAllAsync();
        return Ok(results.ToJson());
    }
}