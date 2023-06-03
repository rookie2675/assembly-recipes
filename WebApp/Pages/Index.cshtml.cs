using Domain;

using Microsoft.AspNetCore.Mvc.RazorPages;

using Services.Contracts;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {

        private readonly IRecipeService _recipeService;

        internal List<Recipe> Recipes { get; init; }

        public IndexModel(IRecipeService recipeService)
        {
            Recipes = new();
            _recipeService = recipeService;
        }

        public void OnGet() => _recipeService.GetRecipes();
    }
}