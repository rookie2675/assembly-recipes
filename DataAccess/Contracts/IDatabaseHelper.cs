using Microsoft.Data.SqlClient;

namespace DataAccess.Contracts
{
    public interface IDatabaseHelper 
    {
        void ExecuteNonQuery(string query, SqlParameter[] parameters);
        
        object ExecuteScalar(string query, SqlParameter[] parameters);

        SqlDataReader ExecuteQuery(string query, SqlParameter[] parameters);
    }
}