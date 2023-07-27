using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;

namespace ProgressPalsAPI.Tests.Integration;

public class Tests
{
    private const string _userPoolId = "eu-north-1_SBEjdNmBU";
    private const string _clientId = "223uolj05eekfvagtjqee0571p";
    private readonly AmazonCognitoIdentityProviderClient _cognitoClient;

    public Tests()
    {
        _cognitoClient = new AmazonCognitoIdentityProviderClient(RegionEndpoint.EUNorth1);
    }

    [Test]
    public async Task CanCreateUser()
    {
        var request = new SignUpRequest
        {
            ClientId = _clientId,
            Password = "TestUser12345!", // Make sure this aligns with your password policy
            Username = "testuser12345", // Using a non-email format string as the username

            UserAttributes = new List<AttributeType>
            {
                new AttributeType { Name = "email", Value = "testuser23@example.com" },
                new AttributeType { Name = "gender", Value = "male" },
                new AttributeType { Name = "name", Value = "Test User12" },
                new AttributeType { Name = "birthdate", Value = "2000-01-02" },
                //new AttributeType { Name = "preferred_username", Value = "testuser1232" }
            }
        };

        SignUpResponse response = null;
        try
        {
            response = await _cognitoClient.SignUpAsync(request);
            Assert.NotNull(response);
            Assert.True(response.HttpStatusCode == System.Net.HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            Assert.False(true, ex.Message);
        }
    }

    [Test]
    public async Task UserLogin()
    {
        // Make sure to use the same values you used to create the user
        string username = "testuser12345";
        string password = "TestUser12345!";

        var request = new InitiateAuthRequest
        {
            ClientId = _clientId,
            AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
            AuthParameters = new Dictionary<string, string>
        {
            {"USERNAME", username},
            {"PASSWORD", password}
        }
        };

        InitiateAuthResponse response = null;
        try
        {
            response = await _cognitoClient.InitiateAuthAsync(request);
            Assert.NotNull(response);
            Assert.NotNull(response.AuthenticationResult); // Successful authentication returns a result containing tokens
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }



}
