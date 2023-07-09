using Domain;

using Microsoft.Data.SqlClient;

using Repositories.Contracts;

using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string connectionString;

        public UserRepository(string connectionString) => this.connectionString = connectionString;

        public User? Find(long id)
        {
            if (id <= 0) throw new ArgumentException("ID must be a positive non-zero value.", nameof(id));

            using SqlConnection connection = new(connectionString);
            connection.Open();

            string query = "SELECT Id, Username, Password FROM Users WHERE Id = @Id";

            using SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                ushort foundId = (ushort)reader["Id"];
                string foundUsername = (string)reader["Username"];
                string foundPassword = (string)reader["Password"];

                return new User { Id = foundId, Username = foundUsername, Password = foundPassword };
            }

            return null;
        }

        public User? Find(string username, string password)
        {

            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username cannot be null or empty.", nameof(username));

            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            using SqlConnection connection = new(connectionString);
            connection.Open();

            string query = "SELECT Id, Username, Password FROM Users WHERE Username = @Username AND Password = @Password";

            using SqlCommand command = new(query, connection);
            command.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username;
            command.Parameters.Add("@Password", SqlDbType.NVarChar, 100).Value = password;

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                long id = (long)reader["Id"];
                string foundUsername = (string)reader["Username"];
                string foundPassword = (string)reader["Password"];

                return new User { Id = id, Username = foundUsername, Password = foundPassword };
            }

            return null;
        }

        public List<User> Find()
        {
            List<User> users = new();

            using SqlConnection connection = new(connectionString);
            connection.Open();

            string query = "SELECT Id, Username, Password FROM Users";

            using SqlCommand command = new(query, connection);

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                long id = (long)reader["Id"];
                string username = (string)reader["Username"];
                string password = (string)reader["Password"];

                User user = new() { Id = id, Username = username, Password = password };
                users.Add(user);
            }

            return users;
        }

        public List<User> Find(int count)
        {
            if (count <= 0)
                throw new ArgumentException("Count must be a positive non-zero value.", nameof(count));

            var users = new List<User>();

            using SqlConnection connection = new(connectionString);
            connection.Open();

            string query = $"SELECT TOP {count} Id, Username, Password FROM Users";

            using SqlCommand command = new(query, connection);

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                long id = (long)reader["Id"];
                string username = (string)reader["Username"];
                string password = (string)reader["Password"];

                var user = new User { Id = id, Username = username, Password = password };
                users.Add(user);
            }

            return users;
        }

        public User Add(User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user), "User cannot be null.");

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(user, serviceProvider: null, items: null);
            bool isValid = Validator.TryValidateObject(user, validationContext, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                string validationErrors = string.Join(Environment.NewLine, validationResults.Select(vr => vr.ErrorMessage));
                throw new ArgumentException("User is not valid. Validation errors: " + validationErrors);
            }

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password); SELECT SCOPE_IDENTITY();";

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);

                long id = Convert.ToInt64(command.ExecuteScalar());
                user.Id = id;
            }

            return user;
        }

        public User? Update(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user), "User cannot be null.");

            if (string.IsNullOrWhiteSpace(user.Password)) throw new ArgumentException("Password cannot be null or empty.");

            using SqlConnection connection = new(connectionString);
            connection.Open();

            string query = "UPDATE Users SET Password = @Password WHERE Id = @Id";

            using SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Id", user.Id);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0) return user;

            return null;
        }

        public User Delete(long id) => throw new NotImplementedException();
    }
}