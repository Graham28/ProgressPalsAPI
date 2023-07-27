using Amazon.CognitoIdentityProvider.Model;
using ProgressPalsAPI.Domain.User;

namespace ProgressPalsAPI.Cognito.Interfaces
{
    public interface ICognitoClient
    {
        Task<SignUpResponse> CreateUserAsync(User user);
        Task<InitiateAuthResponse> UserLoginAsync(string username, string password);
    }
}