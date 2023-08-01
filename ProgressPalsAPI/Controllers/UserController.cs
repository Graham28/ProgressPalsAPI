using Microsoft.AspNetCore.Mvc;
using ProgressPalsAPI.Domain.User;
using ProgressPalsAPI.Cognito.Interfaces;
using Amazon.CognitoIdentityProvider.Model;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ICognitoClient _cognitoClient;
    private readonly IAuthenticationResultCache _authCache;

    public UserController(ICognitoClient cognitoClient, IAuthenticationResultCache authCache)
    {
        _cognitoClient = cognitoClient;
        _authCache = authCache;
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
                await _authCache.SetAuthenticationResultAsync(user.Email, result.AuthenticationResult);

                return Ok(new LoginDetails { UserIdentifier = user.Email, Token = result.AuthenticationResult.AccessToken });
            }

            return BadRequest(new { Message = "Failed to login user." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("UserData")]
    public async Task<IActionResult> GetUserData([FromBody] LoginDetails loginDetails)
    {
        var sessionIsValid = await _authCache.VerifySession(loginDetails);
        if (sessionIsValid)
        {
            //Make call to get user data
            // Just as a placeholder for now
            var userData = new { Name = "John Doe" };
            return Ok(userData);
        }
        return Unauthorized(new { Message = "Invalid session token." });
    }

}
