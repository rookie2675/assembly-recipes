using Microsoft.Data.SqlClient;

namespace DataAccess.Contracts
{
    public interface IDatabaseHelper 
    {
        void ExecuteNonQuery(string query, SqlParameter[] parameters);

        void ExecuteNonQuery(string query, SqlParameter parameter);

        public T ExecuteScalar<T>(string query, SqlParameter[] parameters);

        public T ExecuteScalar<T>(string query);

        SqlDataReader ExecuteQuery(string query, SqlParameter[] parameters);

        SqlDataReader ExecuteQuery(string query, SqlParameter parameter);

        SqlDataReader ExecuteQuery(string query);
    }
}