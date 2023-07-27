using System;
namespace ProgressPalsAPI.Domain.Settings
{
    public class CognitoClientSettings
    {
        public required string ClientId { get; set; }
        public required string UserPoolId { get; set; }
        public required string Region { get; set; }
    }
}

