using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class RecipeModel : PageModel
    {
        public Recipe Recipe { get; set; }

        public RecipeModel() 
        {
            Recipe = new Recipe
            {
                Id = 1,
                Name = "Chocolate Cake",
                Description = "A delicious homemade chocolate cake.",
                ShortDescription = "Delicious chocolate cake",

                Steps = new List<string>
                {
                    "Preheat the oven to 350°F (175°C).",
                    "Mix all the dry ingredients in a large bowl.",
                    "Add the wet ingredients and mix well until combined.",
                    "Pour the batter into a greased baking pan.",
                    "Bake for 30-35 minutes or until a toothpick inserted in the center comes out clean.",
                    "Let it cool, and then frost wi th your favorite chocolate frosting.",
                    "Serve and enjoy!"
                },

                Ingredients = new List<string>
                {
                    "2 cups all-purpose flour",
                    "1 3/4 cups granulated sugar",
                    "3/4 cup unsweetened cocoa powder",
                    "1 1/2 teaspoons baking powder",
                    "1 1/2 teaspoons baking soda",
                    "1 teaspoon salt",
                    "2 large eggs",
                    "1 cup milk",
                    "1/2 cup vegetable oil",
                    "2 teaspoons vanilla extract",
                    "1 cup boiling water"
                }
            };
        }
    }
}
