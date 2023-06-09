using Domain;

using Microsoft.Data.SqlClient;

using Repositories.Contracts;

using System.Data;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string connectionString;

        public UserRepository(string connectionString) => this.connectionString = connectionString;

        public User Find(long id) => throw new NotImplementedException();

        public User? Find(string username, string password)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "SELECT Id, Username, Password FROM Users WHERE Username = @Username AND Password = @Password";

            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username;
            command.Parameters.Add("@Password", SqlDbType.NVarChar, 100).Value = password;

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                ushort id = (ushort)reader["Id"];
                string foundUsername = (string)reader["Username"];
                string foundPassword = (string)reader["Password"];

                return new User { Id = id, Username = foundUsername, Password = foundPassword };
            }

            return null;
        }

        public List<User> Find() => throw new NotImplementedException();

        public User Add(User user)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password); SELECT SCOPE_IDENTITY();";
            
            using SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Password", user.Password);

            long id = Convert.ToInt64(command.ExecuteScalar());
            user.Id = id;

            return user;
        }

        public User Update(User user) => throw new NotImplementedException();

        public User Delete(long id) => throw new NotImplementedException();
    }
}