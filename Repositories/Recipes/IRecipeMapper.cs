using Domain;
using Microsoft.Data.SqlClient;

namespace Repositories.Recipes
{
    public interface IRecipeMapper
    {
        Recipe MapReaderToRecipe(SqlDataReader reader);
    }
}