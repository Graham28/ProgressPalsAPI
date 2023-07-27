using Microsoft.AspNetCore.Mvc;
using ProgressPalsAPI.Domain.User;
using ProgressPalsAPI.Cognito.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ICognitoClient _cognitoClient;

    public UserController(ICognitoClient cognitoClient)
    {
        _cognitoClient = cognitoClient;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] User user)
    {
        try
        {
            var result = await _cognitoClient.CreateUserAsync(user);

            if (result.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(new { Message = "User registered successfully." });
            }

            return BadRequest(new { Message = "Failed to register user." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] User user)
    {
        try
        {
            var result = await _cognitoClient.UserLoginAsync(user.Email, user.Password);

            if (result.AuthenticationResult != null)
            {
                return Ok(new { Token = result.AuthenticationResult.AccessToken });
            }

            return BadRequest(new { Message = "Failed to login user." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}
