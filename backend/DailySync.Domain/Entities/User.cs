namespace DailySync.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public User() { }

        public User(string username, string email, string passwordHash, string firstName, string lastName)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
