namespace ProgressPalsAPI.Domain.User
{
    public class User
    {
        public string? Password { get; set; }
        public required string Email { get; set; }
        public Gender? Gender { get; set; }
        public string? Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string? DisplayUsername { get; set; }
    }
}