namespace Domain
{
    public class User
    {
        public required ushort Id { get; set; }

        public required string Username { get; set; }

        public required string Password { get; set; }
    }
}