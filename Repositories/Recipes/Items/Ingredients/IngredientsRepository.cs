using DataAccess.Contracts;
using Domain;
using Microsoft.Data.SqlClient;

namespace Repositories.Recipes.Items.Ingredients
{
    public class IngredientsRepository : IIngredientRepository
    {
        private readonly ISqlQueryExecutor _queryExecutor;

        public IngredientsRepository(ISqlQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor;
        }

        public IEnumerable<string> Find(Recipe recipe)
        {
            string query = "SELECT Ingredient FROM RecipeIngredients WHERE RecipeId = @RecipeId";
            SqlParameter parameter = new SqlParameter("@RecipeId", recipe.Id);

            using SqlDataReader reader = _queryExecutor.ExecuteQuery(query, parameter);
            while (reader.Read())
            {
                string ingredient = reader.GetString(0);
                yield return ingredient;
            }
        }

        public void Add(Recipe recipe, string ingredient)
        {
            string query = "INSERT INTO RecipeIngredients (RecipeId, Ingredient) VALUES (@RecipeId, @Ingredient)";
            SqlParameter[] parameters = {
                new SqlParameter("@RecipeId", recipe.Id),
                new SqlParameter("@Ingredient", ingredient)
            };

            _queryExecutor.ExecuteNonQuery(query, parameters);
        }

        public void Update(Recipe recipe, string oldIngredient, string newIngredient)
        {
            string query = "UPDATE RecipeIngredients SET Ingredient = @NewIngredient WHERE RecipeId = @RecipeId AND Ingredient = @OldIngredient";
            SqlParameter[] parameters = {
                new SqlParameter("@RecipeId", recipe.Id),
                new SqlParameter("@OldIngredient", oldIngredient),
                new SqlParameter("@NewIngredient", newIngredient)
            };

            _queryExecutor.ExecuteNonQuery(query, parameters);
        }

        public void Delete(Recipe recipe, string ingredient)
        {
            string query = "DELETE FROM RecipeIngredients WHERE RecipeId = @RecipeId AND Ingredient = @Ingredient";
            SqlParameter[] parameters = {
                new SqlParameter("@RecipeId", recipe.Id),
                new SqlParameter("@Ingredient", ingredient)
            };

            _queryExecutor.ExecuteNonQuery(query, parameters);
        }
    }
}