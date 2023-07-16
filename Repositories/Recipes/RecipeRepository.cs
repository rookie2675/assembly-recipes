using DataAccess.Contracts;
using Domain;
using Microsoft.Data.SqlClient;

namespace Repositories.Recipes
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ISqlQueryExecutor _databaseHelper;

        public RecipeRepository(ISqlQueryExecutor databaseHelper) => _databaseHelper = databaseHelper;

        public Recipe? FindById(long id)
        {
            string query = "SELECT Id, Name, Description, ShortDescription, ImageURL FROM Recipes WHERE Id = @Id";
            var parameter = new SqlParameter("@Id", id);

            using var reader = _databaseHelper.ExecuteQuery(query, parameter);

            if (reader.Read())
            {
                var recipe = CreateRecipeFromReader(reader);
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
                    var recipe = CreateRecipeFromReader(reader);
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
                yield return CreateRecipeFromReader(reader);
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

        private static Recipe CreateRecipeFromReader(SqlDataReader reader)
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            string description = reader.GetString(2);
            string shortDescription = reader.GetString(3);
            string imageURL = reader.GetString(4);

            var recipe = new Recipe()
            {
                Id = id,
                Name = name,
                Description = description,
                ShortDescription = shortDescription,
                ImageURL = imageURL
            };

            return recipe;
        }

        public int GetTotalCount()
        {
            string query = "SELECT COUNT(*) FROM Recipes";
            int totalCount = _databaseHelper.ExecuteScalar<int>(query);

            return totalCount;
        }
    }
}