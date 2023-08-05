using DataAccess.Contracts;
using Domain;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace Repositories.Recipes
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly IRecipeMapper _recipeMapper;
        private readonly ISqlQueryExecutor _databaseHelper;

        public RecipeRepository(IRecipeMapper recipeMapper, ISqlQueryExecutor databaseHelper)
        {
            _recipeMapper = recipeMapper;
            _databaseHelper = databaseHelper;
        }

        public Recipe? FindById(long id)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be a positive non-zero value.", nameof(id));

            string query = "SELECT Id, Name, Description, ShortDescription, ImageURL FROM Recipes WHERE Id = @Id";
            var parameter = new SqlParameter("@Id", id);

            Debug.WriteLine($"Executing FindById with ID: {id}");

            using var reader = _databaseHelper.ExecuteQuery(query, parameter);

            if (reader.Read())
            {
                var recipe = _recipeMapper.MapReaderToRecipe(reader);
                return recipe;
            }

            return null;
        }

        public List<Recipe> FindAll()
        {
            var recipes = new List<Recipe>();

            string query = "SELECT Id, Name, Description, ShortDescription, ImageURL FROM Recipes";

            using (var reader = _databaseHelper.ExecuteQuery(query))
            {
                while (reader.Read())
                {
                    var recipe = _recipeMapper.MapReaderToRecipe(reader);
                    recipes.Add(recipe);
                }
            }

            return recipes;
        }

        public IEnumerable<Recipe> FindPage(int page, int size)
        {
            int skipCount = (page - 1) * size;

            string query = "SELECT Id, Name, Description, ShortDescription, ImageURL FROM Recipes " +
                           "ORDER BY Id OFFSET @Skip ROWS FETCH NEXT @Size ROWS ONLY";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Skip", skipCount),
                new SqlParameter("@Size", size)
            };

            using var reader = _databaseHelper.ExecuteQuery(query, parameters);

            while (reader.Read())
                yield return _recipeMapper.MapReaderToRecipe(reader);
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

            recipe.Id = Convert.ToInt32(_databaseHelper.ExecuteScalar<long>(query, parameters));

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

            _databaseHelper.ExecuteNonQuery(query, parameters);

            return recipe;
        }

        public Recipe Delete(long id)
        {
            var deletedRecipe = FindById(id) ?? throw new InvalidOperationException("Recipe not found");

            string query = "DELETE FROM Recipes WHERE Id = @Id";

            SqlParameter parameter = new("@Id", id);

            _databaseHelper.ExecuteNonQuery(query, parameter);

            return deletedRecipe;
        }

        public int GetTotalCount()
        {
            string query = "SELECT COUNT(*) FROM Recipes";
            int totalCount = _databaseHelper.ExecuteScalar<int>(query);

            return totalCount;
        }
    }
}