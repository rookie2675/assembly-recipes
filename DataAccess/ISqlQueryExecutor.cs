using Microsoft.Data.SqlClient;

namespace DataAccess.Contracts
{
    public interface ISqlQueryExecutor
    {
        SqlDataReader ExecuteQuery(string query, SqlParameter[] parameters);

        SqlDataReader ExecuteQuery(string query, SqlParameter parameter);

        SqlDataReader ExecuteQuery(string query);

        public T? ExecuteScalar<T>(string query, SqlParameter[] parameters);

        public T? ExecuteScalar<T>(string query);

        void ExecuteNonQuery(string query, SqlParameter[] parameters);

        void ExecuteNonQuery(string query, SqlParameter parameter);
    }
}