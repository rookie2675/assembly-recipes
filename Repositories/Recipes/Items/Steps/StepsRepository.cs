using Domain;
using Microsoft.Data.SqlClient;

namespace Repositories.Recipes.Items.Steps
{
    public class StepsRepository : IStepsRepository
    {
        private readonly string connectionString;

        public StepsRepository(string connectionString) => this.connectionString = connectionString;

        public List<string> Find(Recipe recipe)
        {
            var ingredients = new List<string>();

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "SELECT Ingredient FROM RecipeIngredients WHERE RecipeId = @RecipeId";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@RecipeId", recipe.Id);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ingredient = reader.GetString(0);
                ingredients.Add(ingredient);
            }

            return ingredients;
        }
    }
}