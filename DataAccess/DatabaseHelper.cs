using DataAccess.Contracts;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(string connectionString) => _connectionString = connectionString;

        public SqlDataReader ExecuteQuery(string query, SqlParameter[] parameters)
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand(query, connection);
            command.Parameters.AddRange(parameters);

            return command.ExecuteReader();
        }

        public SqlDataReader ExecuteQuery(string query)
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand(query, connection);

            return command.ExecuteReader();
        }

        public SqlDataReader ExecuteQuery(string query, SqlParameter parameter)
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand(query, connection);
            command.Parameters.Add(parameter);

            return command.ExecuteReader();
        }

        public void ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand command = new(query, connection);
            command.Parameters.AddRange(parameters);

            command.ExecuteNonQuery();
        }

        public void ExecuteNonQuery(string query)
        {
            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public T? ExecuteScalar<T>(string query, SqlParameter[] parameters)
        {
            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(query, connection);

            if (parameters != null)
                command.Parameters.AddRange(parameters.Where(p => p != null).ToArray());

            connection.Open();
            var result = command.ExecuteScalar();

            if (result != null && result != DBNull.Value)
                return (T)Convert.ChangeType(result, typeof(T));

            return default;
        }

        public T? ExecuteScalar<T>(string query)
        {
            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(query, connection);
            connection.Open();
            var result = command.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                return (T)Convert.ChangeType(result, typeof(T));
            }
            else
            {
                return default;
            }
        }

        public void ExecuteNonQuery(string query, SqlParameter parameter)
        {
            throw new NotImplementedException();
        }
    }
}
