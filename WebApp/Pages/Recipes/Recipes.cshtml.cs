using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;

namespace WebApp.Pages.Recipes
{
    public class RecipesModel : PageModel
    {
        private readonly IRecipeService _recipeService;

        public IEnumerable<Recipe> Recipes { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool IsAuthenticated { get; set; }

        public RecipesModel(IRecipeService recipeService) => _recipeService = recipeService;

        public void OnGet(int page)
        {
            var pageSize = 9;
            CurrentPage = page > 0 ? page : 1;

            Recipes = _recipeService.GetPage(CurrentPage, pageSize);
            int totalRecipes = _recipeService.GetTotalCount();
            TotalPages = (int)Math.Ceiling((double)totalRecipes / pageSize);
        }

        public bool IsUserLoggedIn() => HttpContext.Session.GetInt32("UserId") is not null;
    }
}