using Microsoft.AspNetCore.Mvc;
using Mongo.TestContainer.Models.Constants;
using Mongo.TestContainer.Models.Logs;
using Testcontainers.MongoDb;

namespace Mongo.TestContainer.Controllers;

public sealed class LogsController([FromKeyedServices(TestContainerKeys.MongoDbContainerInstanceKey)] MongoDbContainer mongoContainer) : ControllerBase
{
    private readonly MongoDbContainer _mongoContainer = mongoContainer;

    [HttpGet]
    [Route("api/logs")]
    public async Task<IActionResult> GetLogs()
    {
        var (stdout, stderr) = await _mongoContainer.GetLogsAsync();
        return Ok(new ContainerLogs(stdout, stderr));
    }
}
