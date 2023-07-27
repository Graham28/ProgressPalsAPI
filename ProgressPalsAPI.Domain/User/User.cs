using System;
using ProgressPalsAPI.Domain.User;

namespace ProgressPalsAPI.Domain.User
{
    public class User
    {
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required Gender Gender { get; set; }
        public required string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public required string DisplayUsername { get; set; }
    }
}

