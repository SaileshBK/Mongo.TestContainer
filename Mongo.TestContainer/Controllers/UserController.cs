using Microsoft.AspNetCore.Mvc;
using Mongo.TestContainer.Models.Constants;
using Mongo.TestContainer.Services.Interfaces;


namespace Mongo.TestContainer.Controllers;

public sealed class UserController(IMongoDbService mongoDbService) : ControllerBase
{
    private readonly IMongoDbService _mongoDbService = mongoDbService;

    [HttpGet]
    [Route("api/user-list")]
    public IActionResult Get()
    {
        var test = _mongoDbService.GetDatabase(TestContainerKeys.TestContainerDatabase);

        return Ok("yes");
    }
}
