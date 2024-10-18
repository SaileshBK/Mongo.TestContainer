using Microsoft.AspNetCore.Mvc;
using Mongo.TestContainer.Models.Constants;
using Testcontainers.MongoDb;

namespace Mongo.TestContainer.Controllers;

public sealed class LogsController([FromKeyedServices(TestContainerKeys.MongoDbContainerInstanceKey)] MongoDbContainer mongoContainer) : ControllerBase
{
    private readonly MongoDbContainer _mongoContainer = mongoContainer;

    [HttpGet]
    [Route("api/container/logs")]
    public async Task<IActionResult> GetContainerLogs()
    {
        var (stdout, _) = await _mongoContainer.GetLogsAsync();
        return Ok(stdout);
    }

    [HttpGet]
    [Route("api/container/error")]
    public async Task<IActionResult> GetContainerErrors()
    {
        var (_, error) = await _mongoContainer.GetLogsAsync();
        return Ok(error);
    }
}
