
using Domain;

using Microsoft.AspNetCore.Mvc.RazorPages;

using Services.Contracts;

namespace WebApp.Pages
{
    public class RecipesModel : PageModel
    {
        private readonly IRecipeService _recipeService;

        public List<Recipe> Recipes { get; set; }
        public int CurrentPage { get; set; }

        public RecipesModel(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public void OnGet(int page)
        {
            Recipes = _recipeService.Find();

            CurrentPage = page;
        }
    }
}