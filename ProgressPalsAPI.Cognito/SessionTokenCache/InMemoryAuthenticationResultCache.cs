using Amazon.CognitoIdentityProvider.Model;
using Microsoft.Extensions.Caching.Memory;
using ProgressPalsAPI.Cognito.Interfaces;
using ProgressPalsAPI.Domain.User;

namespace ProgressPalsAPI.Cognito.SessionTokenCache
{
    public class InMemoryAuthenticationResultCache : IAuthenticationResultCache
    {
        private readonly IMemoryCache _cache;

        public InMemoryAuthenticationResultCache()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        public async Task<AuthenticationResultType> GetAuthenticationResultAsync(string userIdentifier)
        {
            if (_cache.TryGetValue(userIdentifier, out AuthenticationResultType authResult))
            {
                return await Task.FromResult(authResult);
            }
            return null;
        }

        public async Task SetAuthenticationResultAsync(string userIdentifier, AuthenticationResultType authResult, TimeSpan? expiration = null)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromHours(1) // Default to 1 hour if not provided
            };

            _cache.Set(userIdentifier, authResult, cacheEntryOptions);
            await Task.CompletedTask;
        }

        public async Task RemoveAuthenticationResultAsync(string userIdentifier)
        {
            _cache.Remove(userIdentifier);
            await Task.CompletedTask;
        }

        public async Task<bool> AuthenticationResultExistsAsync(string userIdentifier)
        {
            return await Task.FromResult(_cache.TryGetValue(userIdentifier, out _));
        }

        public async Task<bool> VerifySession(LoginDetails loginDetails)
        {
            var cachedAuthResult = await GetAuthenticationResultAsync(loginDetails.UserIdentifier);

            if (cachedAuthResult == null)
            {
                return false;
            }

            // Compare the token in the cache with the token provided by the client
            return cachedAuthResult.AccessToken == loginDetails.Token;
        }
    }
}

