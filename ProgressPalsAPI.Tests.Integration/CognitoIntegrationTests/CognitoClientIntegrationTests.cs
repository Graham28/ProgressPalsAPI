using NUnit.Framework;
using ProgressPalsAPI.Domain.Settings;
using ProgressPalsAPI.Domain.User;
using System;
using System.Threading.Tasks;

namespace ProgressPalsAPI.Tests.Integration.CognitoIntegrationTests
{
    [TestFixture]
    public class CognitoClientTests
    {
        private const string _userPoolId = "eu-north-1_tJVli0BDo";
        private const string _clientId = "413d67ndc1d1qn1mekea9n1jk4";
        private CognitoClient _cognitoClient;
        private User _user;

        [OneTimeSetUp]
        public void Setup()
        {
            var settings = new CognitoClientSettings
            {
                ClientId = _clientId,
                UserPoolId = _userPoolId,
                Region = "eu-north-1"
            };

            _cognitoClient = new CognitoClient(settings);
            var random = Guid.NewGuid().ToString();
            _user = new User
            {
                Email = $"testuser24{random}@example.com",
                Password = "TestUser12345!",
                Gender = Gender.Male,
                Name = $"Test User12{random}",
                Birthdate = DateTime.Parse("2000-01-02"),
                DisplayUsername = $"testuser{random.Substring(0, 5)}"
            };
        }

        [Test]
        public async Task CanCreateUserAsync()
        {
            try
            {
                var signUpResponse = await _cognitoClient.CreateUserAsync(_user);
                Assert.NotNull(signUpResponse);
                Assert.True(signUpResponse.HttpStatusCode == System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public async Task CanLoginUserAsync()
        {
            try
            {
                var loginResponse = await _cognitoClient.UserLoginAsync(_user.Email, _user.Password);
                Assert.NotNull(loginResponse);
                Assert.NotNull(loginResponse.AuthenticationResult);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
