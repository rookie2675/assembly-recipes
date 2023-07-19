using DataAccess.Contracts;
using Domain;
using Microsoft.Data.SqlClient;

namespace Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly ISqlQueryExecutor _queryExecutor;

        public UserRepository(string connectionString, ISqlQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor;
            _connectionString = connectionString;
        }

        public User? FindById(long id)
        {
            if (id <= 0) throw new ArgumentException("ID must be a positive non-zero value.", nameof(id));

            string query = "SELECT Id, Username, Password, Email FROM Users WHERE Id = @Id";
            SqlParameter[] parameters = { new SqlParameter("@Id", id) };

            using SqlDataReader reader = _queryExecutor.ExecuteQuery(query, parameters);

            return MapSqlRowToObject(reader);
        }

        public User? FindByUsernameAndPassword(string username, string password)
        {
            User? user = null;

            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username cannot be null or empty.", nameof(username));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";

            SqlParameter[] parameters = 
            {
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password)
            };

            using (SqlDataReader reader = _queryExecutor.ExecuteQuery(query, parameters))
            {
                if (reader.Read())
                {
                    long id = (long)reader["Id"];
                    string foundUsername = (string)reader["Username"];
                    string foundPassword = (string)reader["Password"];
                    string email = (string)reader["Email"];

                    user = new User { Id = id, Username = foundUsername, Password = foundPassword, Email = email };
                }
            }

            return user;
        }

        public List<User> FindAll()
        {
            string query = "SELECT Id, Username, Password FROM Users";

            using SqlDataReader reader = _queryExecutor.ExecuteQuery(query);

            if (reader is null || !reader.IsClosed || !reader.HasRows)
                throw new InvalidDataException("SqlDataReader is null or closed, or empty.");

            var users = new List<User>();

            if (reader.Read())
            {
                User user = MapSqlRowToObject(reader);
                users.Add(user);
            }

            return users;
        }

        public IEnumerable<User> FindPage(int page, int pageSize)
        {
            if (page <= 0)
                throw new ArgumentException("Page must be a positive non-zero value.", nameof(page));

            if (pageSize <= 0)
                throw new ArgumentException("PageSize must be a positive non-zero value.", nameof(pageSize));

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string countQuery = "SELECT COUNT(*) FROM Users";
            int totalCount = (int)_queryExecutor.ExecuteScalar<long>(countQuery, new SqlParameter[1]);

            string query = "SELECT Id, Username, Password, Email FROM Users " +
                           "ORDER BY Id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            int offset = (page - 1) * pageSize;
            SqlParameter[] parameters =
            {
                new SqlParameter("@Offset", offset),
                new SqlParameter("@PageSize", pageSize)
            };

            using SqlDataReader reader = _queryExecutor.ExecuteQuery(query, parameters);

            if (reader is null || !reader.IsClosed || !reader.HasRows)
                throw new InvalidDataException("SqlDataReader is null or closed, or empty.");

            var users = new List<User>();

            if (reader.Read())
            {
                User user = MapSqlRowToObject(reader);
                users.Add(user);
            }

            return users;
        }

        public User Add(User user)
        {
            if (user is null) 
                throw new ArgumentNullException(nameof(user), "User cannot be null.");

            string query = "INSERT INTO Users (Username, Password, Role, Email) VALUES (@Username, @Password, @Role, @Email); SELECT SCOPE_IDENTITY();";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@Role", "user"),
                new SqlParameter("@Email", user.Email)
            };

            long id = Convert.ToInt64(_queryExecutor.ExecuteScalar<long>(query, parameters));
            user.Id = id;

            return user;
        }

        public User Update(User user)
        {
            if (user == null) 
                throw new ArgumentNullException(nameof(user), "User cannot be null.");

            if (string.IsNullOrWhiteSpace(user.Password)) 
                throw new ArgumentException("Password cannot be null or empty.");

            using SqlConnection connection = new(_connectionString);
            connection.Open();

            string query = "UPDATE Users SET Password = @Password WHERE Id = @Id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@Id", user.Id)
            };

            _queryExecutor.ExecuteNonQuery(query, parameters);

            return user;
        }

        public User Delete(long id) => throw new NotImplementedException();

        public int GetTotalCount() => throw new NotImplementedException();

        public bool DoesUsernameExist(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be null or empty.", nameof(username));

            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";

            SqlParameter[] parameters = { new SqlParameter("@Username", username) };

            int count = _queryExecutor.ExecuteScalar<int>(query, parameters);

            return count > 0;
        }

        public bool DoesEmailExist(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";

            SqlParameter[] parameters = { new SqlParameter("Email", email) };

            int count = _queryExecutor.ExecuteScalar<int>(query, parameters);

            return count > 0;
        }

        private static User MapSqlRowToObject(SqlDataReader reader)
        {
            if (reader is null || !reader.IsClosed || !reader.HasRows)
                throw new InvalidDataException("SqlDataReader is null or closed, or empty.");

            long id = (long)reader["Id"];
            string username = (string)reader["Username"];
            string password = (string)reader["Password"];
            string email = (string)reader["Email"];

            return new User { Id = id, Username = username, Password = password, Email = email };
        }
    }
}