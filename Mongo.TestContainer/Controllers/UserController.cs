using Microsoft.AspNetCore.Mvc;

namespace Mongo.TestContainer.Controllers;

public class UserController : ControllerBase
{
    public UserController()
    {
        
    }

    [HttpGet]
    [Route("api/user-list")]
    public string Get()
    {
        return "user";
    }
}
