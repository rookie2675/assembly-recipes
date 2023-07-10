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

        public RecipesModel(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public void OnGet(int page)
        {
            Recipes = _recipeService.Find();

            var pageSize = 9;
            TotalPages = (int)Math.Ceiling((double)Recipes.ToList().Count / pageSize);
            CurrentPage = page > 0 && page <= TotalPages ? page : 1;

            Recipes = Recipes.Skip((CurrentPage - 1) * pageSize).Take(pageSize);
        }

        public bool IsUserLoggedIn() => HttpContext.Session.GetInt32("UserId") is not null;
    }
}