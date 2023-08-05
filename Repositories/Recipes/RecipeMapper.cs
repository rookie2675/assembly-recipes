using Domain;
using Microsoft.Data.SqlClient;

namespace Repositories.Recipes
{
    public class RecipeMapper : IRecipeMapper
    {
        private static readonly string _defaultImageURL = "default-recipe-image.jpg";

        public Recipe MapReaderToRecipe(SqlDataReader reader)
        {
            long id = reader.GetInt64(0);
            string name = reader.GetString(1);
            string description = reader.GetString(2);
            string shortDescription = reader.GetString(3);
            string imageURL = MapImage(reader);

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

        private static string MapImage(SqlDataReader reader) => reader.IsDBNull(4) ? _defaultImageURL : reader.GetString(4);
    }
}
