using Microsoft.AspNetCore.Mvc;

namespace ArtilleryApi.Controllers;

[Route("[controller]")]
[ApiController]
public class HelloController : ControllerBase
{
    private readonly ILogger<HelloController> _logger;

    public HelloController(ILogger<HelloController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Return), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Hello([FromHeader(Name = "user")] string user, [FromHeader(Name = "User-Agent")] string userAgent)
    {

        if (string.IsNullOrEmpty(user))
        {
            return BadRequest("User header is missing");
        }

        return Ok(new Return { Username = user, UserAgent = userAgent });
    }
}

public class Return
{
    public string Username { get; set; }
    public string UserAgent { get; set; }
}

