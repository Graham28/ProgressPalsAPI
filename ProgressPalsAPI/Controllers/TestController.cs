using Microsoft.AspNetCore.Mvc;

namespace ProgressPalsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{ 

    [HttpGet(Name = "GetString")]
    public string Get()
    {
        return "Test works";
    }
}
