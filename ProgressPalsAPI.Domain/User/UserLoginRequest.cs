using System;
namespace ProgressPalsAPI.Domain.User
{
    public class UserLoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}

