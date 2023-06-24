using Domain;

using Microsoft.Data.SqlClient;

using Repositories.Contracts;

namespace DataSqlServer
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly string connectionString;

        public RecipeRepository(string connectionString) => this.connectionString = connectionString;

        public Recipe? Find(long id)
        {
            Recipe recipe;

            using SqlConnection connection = new(connectionString);
            connection.Open();

            string query = "SELECT Id, Name, Description, ShortDescription FROM Recipes WHERE Id = @Id";

            using SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                string name = reader.GetString(1);
                string description = reader.GetString(2);
                string shortDescription = reader.GetString(3);

                recipe = new Recipe
                {
                    Id = id,
                    Name = name,
                    Description = description,
                    ShortDescription = shortDescription,
                    ImageURL = "URL"
                };

                return recipe;
            }

            return null;
        }

        public List<Recipe> Find()
        {
            List<Recipe> recipes = new();

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Name, Description, ShortDescription FROM Recipes";

                using SqlCommand command = new(query, connection);
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string description = reader.GetString(2);
                    string shortDescription = reader.GetString(3);

                    Recipe recipe = new()
                    {
                        Id = id,
                        Name = name,
                        Description = description,
                        ShortDescription = shortDescription,
                        ImageURL = "URL"
                    };
                    recipes.Add(recipe);
                }
            }

            return recipes;
        }

        public Recipe Add(Recipe recipe)
        {
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Recipes (Name, Description, ShortDescription) " +
                               "VALUES (@Name, @Description, @ShortDescription);" +
                               "SELECT SCOPE_IDENTITY();";

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Name", recipe.Name);
                command.Parameters.AddWithValue("@Description", recipe.Description);
                command.Parameters.AddWithValue("@ShortDescription", recipe.ShortDescription);

                recipe.Id = Convert.ToInt32(command.ExecuteScalar());
            }

            return recipe;
        }

        public Recipe Update(Recipe recipe)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();

            string query = "UPDATE Recipes SET Name = @Name, Description = @Description, " +
                            "ShortDescription = @ShortDescription WHERE Id = @Id";

            using SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@Name", recipe.Name);
            command.Parameters.AddWithValue("@Description", recipe.Description);
            command.Parameters.AddWithValue("@ShortDescription", recipe.ShortDescription);
            command.Parameters.AddWithValue("@Id", recipe.Id);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0) throw new InvalidOperationException("Recipe not found or update failed");

            return recipe;
        }

        public Recipe Delete(long id)
        {
            Recipe? deletedRecipe = Find(id) ?? throw new InvalidOperationException("Recipe not found");

            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Recipes WHERE Id = @Id";

                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }

            return deletedRecipe;
        }
    }
}