using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using ProgressPalsAPI.Cognito.Interfaces;
using ProgressPalsAPI.Domain.Settings;
using ProgressPalsAPI.Domain.User;

public class CognitoClient : ICognitoClient
{
    private readonly IAmazonCognitoIdentityProvider _cognitoProvider;
    private readonly CognitoClientSettings _settings;

    public CognitoClient(CognitoClientSettings settings)
    {
        _settings = settings;
        _cognitoProvider = new AmazonCognitoIdentityProviderClient(RegionEndpoint.GetBySystemName(settings.Region));
    }


    public async Task<SignUpResponse> CreateUserAsync(User user)
    {
        var request = new SignUpRequest
        {
            ClientId = _settings.ClientId,
            Password = user.Password,
            Username = user.Email,
            UserAttributes = new List<AttributeType>
            {
                new AttributeType { Name = "email", Value = user.Email },
                new AttributeType { Name = "name", Value = user.Name },
                new AttributeType { Name = "birthdate", Value = user.Birthdate.ToString("yyyy-MM-dd") },
                new AttributeType { Name = "gender", Value = user.Gender.ToString() },
                new AttributeType { Name = "custom:display_username", Value = user.DisplayUsername }
            }
        };

        return await _cognitoProvider.SignUpAsync(request);
    }

    public async Task<InitiateAuthResponse> UserLoginAsync(string username, string password)
    {
        var request = new InitiateAuthRequest
        {
            ClientId = _settings.ClientId,
            AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
            AuthParameters = new Dictionary<string, string>
            {
                {"USERNAME", username},
                {"PASSWORD", password}
            }
        };

        return await _cognitoProvider.InitiateAuthAsync(request);
    }
}