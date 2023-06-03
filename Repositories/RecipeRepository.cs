using Domain;
using Repositories.Contracts;

namespace DataSqlServer
{
    public class RecipeRepository : IRecipeRepository
    {
        public Recipe Add(Recipe entity)
        {
            throw new NotImplementedException();
        }

        public Recipe Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Recipe Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Recipe> GetAll()
        {
            List<Recipe> recipes = new();

            using (SqlConnection connection = new("Your_Connection_String"))
            {
                connection.Open();

                string query = "SELECT Id, Name, Description, ShortDescription FROM Recipes";

                using SqlCommand command = new(query, connection);
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    short id = reader.GetInt16(0);
                    string name = reader.GetString(1);
                    string shortDescription = reader.GetString(3);

                    Recipe recipe = new()
                    {
                        Id = id,
                        Name = name,
                        ShortDescription = shortDescription
                    };
                    recipes.Add(recipe);
                }
            }

            return recipes;
        }

        public Recipe Update(Recipe entity)
        {
            throw new NotImplementedException();
        }
    }
}
