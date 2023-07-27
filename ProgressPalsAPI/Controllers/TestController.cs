using Microsoft.AspNetCore.Mvc;

namespace ProgressPalsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public TestController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetString")]
    public string Get()
    {
        return "Test works";
    }
}

