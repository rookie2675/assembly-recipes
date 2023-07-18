using DataAccess.Contracts;
using Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Repositories.Recipes.Items.Ingredients
{
    public class IngredientsRepository : IIngredientRepository
    {
        private readonly ISqlQueryExecutor _queryExecutor;
        private readonly ILogger<IngredientsRepository> _logger;

        public IngredientsRepository(ILogger<IngredientsRepository> logger, ISqlQueryExecutor queryExecutor) {

            _logger = logger;
            _queryExecutor = queryExecutor;
        }

        public ICollection<Ingredient> FindAllByRecipe(Recipe recipe)
        {
            if (recipe == null)
                throw new ArgumentNullException(nameof(recipe), "Recipe cannot be null.");

            if (recipe.Id <= 0)
                throw new ArgumentException("Recipe Id should be greater than zero.", nameof(recipe.Id));

            string query = "SELECT RecipeId, Ingredient FROM RecipeIngredients WHERE RecipeId = @RecipeId";
            var parameter = new SqlParameter("@RecipeId", recipe.Id);

            var ingredients = new List<Ingredient>(); 

            using SqlDataReader reader = _queryExecutor.ExecuteQuery(query, parameter);
            while (reader.Read())
            {
                var ingredient = new Ingredient
                {
                    Id = reader.GetInt64(0),
                    Name = reader.GetString(1)
                };
                ingredients.Add(ingredient);    
            }

            _logger.LogInformation($"Found {ingredients.Count} ingredients for RecipeId: {recipe.Id}, RecipeName: {recipe.Name}");
            return ingredients;
        }

        public void Add(Recipe recipe, Ingredient ingredient)
        {
            string query = "INSERT INTO RecipeIngredients (RecipeId, IngredientId, IngredientName) VALUES (@RecipeId, @IngredientId, @IngredientName)";

            SqlParameter[] parameters = {
                new SqlParameter("@RecipeId", recipe.Id),
                new SqlParameter("@IngredientId", ingredient.Id),
                new SqlParameter("@IngredientName", ingredient.Name)
            };

            _queryExecutor.ExecuteNonQuery(query, parameters);
        }

        public void Update(Recipe recipe, Ingredient ingredient)
        {
            string query = "UPDATE RecipeIngredients SET IngredientName = @IngredientName WHERE RecipeId = @RecipeId AND IngredientId = @IngredientId";

            SqlParameter[] parameters = {
                new SqlParameter("@RecipeId", recipe.Id),
                new SqlParameter("@IngredientId", ingredient.Id),
                new SqlParameter("@IngredientName", ingredient.Name)
            };

            _queryExecutor.ExecuteNonQuery(query, parameters);
        }

        public void Delete(Recipe recipe, Ingredient ingredient)
        {
            string query = "DELETE FROM RecipeIngredients WHERE RecipeId = @RecipeId AND IngredientId = @IngredientId";

            SqlParameter[] parameters = {
                new SqlParameter("@RecipeId", recipe.Id),
                new SqlParameter("@IngredientId", ingredient.Id)
            };

            _queryExecutor.ExecuteNonQuery(query, parameters);
        }
    }
}