namespace Domain
{
    public class User : Object

    {
        public long? Id { get; set; }

        public required string Username { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public string? Role { get; set; }
    }
}