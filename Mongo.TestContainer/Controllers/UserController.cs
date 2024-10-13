using Microsoft.AspNetCore.Mvc;
using Mongo.TestContainer.Services.Interfaces;

namespace Mongo.TestContainer.Controllers;

public class UserController : ControllerBase
{
    private readonly IMongoDbService _mongoDbService;

    public UserController(IMongoDbService mongoDbService)
    {
        _mongoDbService = mongoDbService;
    }

    [HttpGet]
    [Route("api/user-list")]
    public IActionResult Get()
    {
        var test = _mongoDbService.GetDatabase("User");

        return Ok("yes");
    }
}
