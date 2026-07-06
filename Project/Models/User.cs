namespace Project.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        private static readonly List<User> _users =
        [
            new() { Id = 1, Username = "admin", Password = "admin123", Email = "admin@example.com", Role = "admin" },
            new() { Id = 2, Username = "user", Password = "user123", Email = "user@example.com", Role = "user" }
        ];

        public static User? Authenticate(string username, string password)
        {
            return _users.FirstOrDefault(u =>
                u.Username == username && u.Password == password);
        }

        public static User? GetById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }
    }
}
