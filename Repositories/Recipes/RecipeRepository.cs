using Domain;

using Microsoft.Data.SqlClient;

using Repositories.Contracts;

using System.Data;

namespace Repositories.Recipes
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly string _connectionString;
        private readonly IRecipeStepRepository _recipeStepRepository;
        private readonly IIngredientRepository _ingredientsRepository;

        public RecipeRepository(string connectionString, IRecipeStepRepository recipeStepRepository, IIngredientRepository ingredientRepository)
        {
            _connectionString = connectionString;
            _recipeStepRepository = recipeStepRepository;
            _ingredientsRepository = ingredientRepository;
        }

        public Recipe? Find(long id)
        {
            string query = "SELECT Id, Name, Description, ShortDescription, ImageURL FROM Recipes WHERE Id = @Id";
            SqlParameter parameter = new("@Id", id);

            using SqlDataReader reader = ExecuteQuery(query, new[] { parameter });

            if (reader.Read())
            {
                string name = reader.GetString(1);
                string description = reader.GetString(2);
                string shortDescription = reader.GetString(3);
                string imageURL = reader.GetString(4);

                Recipe recipe = new()
                {
                    Id = id,
                    Name = name,
                    Description = description,
                    ShortDescription = shortDescription,
                    ImageURL = imageURL,
                };

                foreach (string step in _recipeStepRepository.Find(recipe))
                    recipe.AddStep(step);

                foreach (string ingredient in _ingredientsRepository.Find(recipe))
                    recipe.AddIngredient(ingredient);

                return recipe;
            }

            return null;
        }

        public List<Recipe> Find()
        {
            List<Recipe> recipes = new();

            string query = "SELECT Id, Name, Description, ShortDescription, ImageURL FROM Recipes";

            using (SqlDataReader reader = ExecuteQuery(query, Array.Empty<SqlParameter>()))
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string description = reader.GetString(2);
                    string shortDescription = reader.GetString(3);
                    string imageURL = reader.GetString(4);

                    Recipe recipe = new Recipe
                    {
                        Id = id,
                        Name = name,
                        Description = description,
                        ShortDescription = shortDescription,
                        ImageURL = imageURL
                    };

                    recipes.Add(recipe);
                }
            }

            return recipes;
        }

        public Recipe Add(Recipe recipe)
        {
            string query = "INSERT INTO Recipes (Name, Description, ShortDescription) " +
                           "VALUES (@Name, @Description, @ShortDescription);" +
                           "SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", recipe.Name),
                new SqlParameter("@Description", recipe.Description),
                new SqlParameter("@ShortDescription", recipe.ShortDescription)
            };

            recipe.Id = Convert.ToInt32(ExecuteScalar(query, parameters));

            return recipe;
        }

        public Recipe Update(Recipe recipe)
        {
            string query = "UPDATE Recipes SET Name = @Name, Description = @Description, " +
                            "ShortDescription = @ShortDescription WHERE Id = @Id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", recipe.Name),
                new SqlParameter("@Description", recipe.Description),
                new SqlParameter("@ShortDescription", recipe.ShortDescription),
                new SqlParameter("@Id", recipe.Id)
            };

            ExecuteNonQuery(query, parameters);

            return recipe;
        }

        public Recipe Delete(long id)
        {
            Recipe? deletedRecipe = Find(id) ?? throw new InvalidOperationException("Recipe not found");

            string query = "DELETE FROM Recipes WHERE Id = @Id";

            SqlParameter parameter = new("@Id", id);

            ExecuteNonQuery(query, new[] { parameter });

            return deletedRecipe;
        }

        private SqlDataReader ExecuteQuery(string query, SqlParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddRange(parameters);

            return command.ExecuteReader();
        }

        private void ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            using SqlConnection connection = new(_connectionString);
            connection.Open();

            using SqlCommand command = new(query, connection);
            command.Parameters.AddRange(parameters);

            command.ExecuteNonQuery();
        }

        private object ExecuteScalar(string query, SqlParameter[] parameters)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddRange(parameters);

            connection.Open();
            return command.ExecuteScalar();
        }
    }
}