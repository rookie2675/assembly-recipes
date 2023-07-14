using DataAccess.Contracts;
using Domain;
using Microsoft.Data.SqlClient;

namespace Repositories.Recipes.Items.Steps
{
    public class StepsRepository : IStepsRepository
    {
        private readonly ISqlQueryExecutor _queryExecutor;

        public StepsRepository(ISqlQueryExecutor queryExecutor) => _queryExecutor = queryExecutor;

        public ICollection<Step> FindAllByRecipe(Recipe recipe)
        {
            string query = "SELECT StepId, Description FROM RecipeSteps WHERE RecipeId = @RecipeId";
            var parameter = new SqlParameter("@RecipeId", recipe.Id);

            var steps = new List<Step>();

            using SqlDataReader reader = _queryExecutor.ExecuteQuery(query, parameter);
            while (reader.Read())
            {
                var step = new Step
                {
                    Id = reader.GetInt64(0),
                    Description = reader.GetString(1)
                };
                steps.Add(step); // Add the step to the collection
            }

            return steps; // Return the collection of steps
        }

        public void Add(Recipe recipe, Step step)
        {
            string query = "INSERT INTO RecipeSteps (RecipeId, StepId, Description) VALUES (@RecipeId, @StepId, @Description)";

            SqlParameter[] parameters = {
                new SqlParameter("@RecipeId", recipe.Id),
                new SqlParameter("@StepId", step.Id),
                new SqlParameter("@Description", step.Description)
            };

            _queryExecutor.ExecuteNonQuery(query, parameters);
        }

        public void Update(Recipe recipe, Step step)
        {
            string query = "UPDATE RecipeSteps SET Description = @Description WHERE RecipeId = @RecipeId AND StepId = @StepId";

            SqlParameter[] parameters = {
                new SqlParameter("@RecipeId", recipe.Id),
                new SqlParameter("@StepId", step.Id),
                new SqlParameter("@Description", step.Description)
            };

            _queryExecutor.ExecuteNonQuery(query, parameters);
        }

        public void Delete(Recipe recipe, Step step)
        {
            string query = "DELETE FROM RecipeSteps WHERE RecipeId = @RecipeId AND StepId = @StepId";

            SqlParameter[] parameters = {
                new SqlParameter("@RecipeId", recipe.Id),
                new SqlParameter("@StepId", step.Id)
            };

            _queryExecutor.ExecuteNonQuery(query, parameters);
        }
    }
}
