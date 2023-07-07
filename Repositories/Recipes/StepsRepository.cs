using Domain;

using Microsoft.Data.SqlClient;

using Repositories.Contracts;

namespace Repositories.Recipes
{
    public class StepsRepository : IRecipeStepRepository
    {
        private readonly string connectionString;

        public StepsRepository(string connectionString) => this.connectionString = connectionString;

        public IEnumerable<string> Find(Recipe recipe)
        {
            List<string> steps = new();

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string query = "SELECT Step FROM RecipeSteps WHERE RecipeId = @RecipeId";

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@RecipeId", recipe.Id);

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