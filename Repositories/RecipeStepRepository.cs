using Microsoft.Data.SqlClient;
using Repositories.Contracts;

namespace Repositories
{
    public class RecipeStepRepository : IRecipeStepRepository
    {
        private readonly string connectionString;

        public RecipeStepRepository(string connectionString) => this.connectionString = connectionString;
        
        public List<string> Find(long recipeId)
        {
            List<string> steps = new();

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string query = "SELECT Step FROM RecipeSteps WHERE RecipeId = @RecipeId";

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@RecipeId", recipeId);

                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string step = reader.GetString(0);
                    steps.Add(step);
                }
            }

            return steps;
        }
    }
}
