using Domain;

using Microsoft.Data.SqlClient;

using Repositories.Contracts;

namespace Repositories
{
    public class IngredientsRepository : IIngredientRepository
    {
        private readonly string connectionString;

        public IngredientsRepository(string connectionString) => this.connectionString = connectionString;

        public IEnumerable<string> Find(Recipe recipe)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string query = "SELECT Ingredient FROM RecipeIngredients WHERE RecipeId = @RecipeId";

            using SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@RecipeId", recipe.Id);

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string ingredient = reader.GetString(0);
                yield return ingredient;
            }
        }
    }
}