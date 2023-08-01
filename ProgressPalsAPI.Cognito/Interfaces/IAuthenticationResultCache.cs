using Amazon.CognitoIdentityProvider.Model;
using ProgressPalsAPI.Domain.User;

namespace ProgressPalsAPI.Cognito.Interfaces
{
    public interface IAuthenticationResultCache
    {
        Task<AuthenticationResultType> GetAuthenticationResultAsync(string userIdentifier);
        Task SetAuthenticationResultAsync(string userIdentifier, AuthenticationResultType authResult, TimeSpan? expiration = null);
        Task RemoveAuthenticationResultAsync(string userIdentifier);
        Task<bool> AuthenticationResultExistsAsync(string userIdentifier);
        Task<bool> VerifySession(LoginDetails loginDetails);
    }
}

