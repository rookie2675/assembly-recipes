using DataAccess;
using DataAccess.Contracts;
using Domain;
using Microsoft.Data.SqlClient;
using PagedList;
using Repositories.Contracts;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly IDatabaseHelper _databaseHelper;

        public UserRepository(string connectionString, IDatabaseHelper databaseHelper)
        {
            _connectionString = connectionString;
            _databaseHelper = databaseHelper;
        }

        public User? FindById(long id)
        {
            if (id <= 0) throw new ArgumentException("ID must be a positive non-zero value.", nameof(id));

            string query = "SELECT Id, Username, Password FROM Users WHERE Id = @Id";
            SqlParameter[] parameters = { new SqlParameter("@Id", id) };

            using SqlDataReader reader = _databaseHelper.ExecuteQuery(query, parameters);

            return ReadUserFromReader(reader);
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

            using (SqlDataReader reader = _databaseHelper.ExecuteQuery(query, parameters))
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

            using SqlDataReader reader = _databaseHelper.ExecuteQuery(query);

            return ReadUsersFromReader(reader);
        }

        public IEnumerable<User> FindPaged(int page, int pageSize)
        {
            if (page <= 0)
                throw new ArgumentException("Page must be a positive non-zero value.", nameof(page));

            if (pageSize <= 0)
                throw new ArgumentException("PageSize must be a positive non-zero value.", nameof(pageSize));

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string countQuery = "SELECT COUNT(*) FROM Users";
            int totalCount = (int)_databaseHelper.ExecuteScalar<long>(countQuery, new SqlParameter[1]);

            string query = "SELECT Id, Username, Password, Email FROM Users " +
                           "ORDER BY Id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            int offset = (page - 1) * pageSize;
            SqlParameter[] parameters =
            {
                new SqlParameter("@Offset", offset),
                new SqlParameter("@PageSize", pageSize)
            };

            using SqlDataReader reader = _databaseHelper.ExecuteQuery(query, parameters);

            var users = ReadUsersFromReader(reader);

            return new StaticPagedList<User>(users, page, pageSize, totalCount);
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

            string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password); SELECT SCOPE_IDENTITY();";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@Password", user.Password)
            };

            long id = Convert.ToInt64(_databaseHelper.ExecuteScalar<long>(query, parameters));
            user.Id = id;

            return user;
        }

        public User? Update(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user), "User cannot be null.");

            if (string.IsNullOrWhiteSpace(user.Password)) throw new ArgumentException("Password cannot be null or empty.");

            using SqlConnection connection = new(_connectionString);
            connection.Open();

            string query = "UPDATE Users SET Password = @Password WHERE Id = @Id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Password", user.Password),
                new SqlParameter("@Id", user.Id)
            };

            _databaseHelper.ExecuteNonQuery(query, parameters);

            return user;
        }

        public User Delete(long id) => throw new NotImplementedException();

        private static User? ReadUserFromReader(SqlDataReader reader)
        {
            if (reader.Read())
            {
                long id = (long)reader["Id"];
                string username = (string)reader["Username"];
                string password = (string)reader["Password"];
                string email = (string)reader["Email"];

                return new User { Id = id, Username = username, Password = password, Email = email };
            }

            return null;
        }

        private static List<User> ReadUsersFromReader(SqlDataReader reader)
        {
            var users = new List<User>();

            if (reader != null && !reader.IsClosed)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        long id = (long)reader["Id"];
                        string username = (string)reader["Username"];
                        string password = (string)reader["Password"];
                        string email = (string)reader["Email"]; // Assuming "Email" is the column name in the database table

                        var user = new User { Id = id, Username = username, Password = password, Email = email };
                        users.Add(user);
                    }
                }
            }

            return users;
        }

        public IPagedList<User> FindPage()
        {
            throw new NotImplementedException();
        }

        public IPagedList<User> FindPage(int page, int size)
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }
    }
}