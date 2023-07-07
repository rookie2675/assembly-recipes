using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(string connectionString) => _connectionString = connectionString;

        public SqlDataReader ExecuteQuery(string query, SqlParameter[] parameters)
        {
            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(query, connection);

            PrepareCommand(command, parameters);
            OpenConnection(connection);

            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public void ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(query, connection);

            PrepareCommand(command, parameters);
            OpenConnection(connection);

            command.ExecuteNonQuery();

            CloseConnection(connection);
        }

        public object ExecuteScalar(string query, SqlParameter[] parameters)
        {
            using SqlConnection connection = new(_connectionString);
            using SqlCommand command = new(query, connection);

            PrepareCommand(command, parameters);
            OpenConnection(connection);

            object result = command.ExecuteScalar();

            CloseConnection(connection);

            return result;
        }

        private static void PrepareCommand(SqlCommand command, SqlParameter[] parameters) => command.Parameters.AddRange(parameters);

        private static void OpenConnection(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }

        private static void CloseConnection(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }
    }
}
