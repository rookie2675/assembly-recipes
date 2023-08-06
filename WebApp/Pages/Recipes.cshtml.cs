using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Recipes;

namespace WebApp.Pages
{
    public class RecipesModel : PageModel
    {
        private readonly IRecipeService _recipeService;

        public required IEnumerable<Recipe> Recipes { get; set; }

        public bool IsUserAuthenticated => User?.Identity?.IsAuthenticated ?? false;

        public RecipesModel(IRecipeService recipeService) => _recipeService = recipeService;

        public void OnGet() => Recipes = _recipeService.GetAll();
    }
}