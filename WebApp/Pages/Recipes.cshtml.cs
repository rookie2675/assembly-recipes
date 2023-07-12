using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;

namespace WebApp.Pages
{
    public class RecipesModel : PageModel
    {
        private readonly IRecipeService _recipeService;

        public required IEnumerable<Recipe> Recipes { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool IsAuthenticated { get; set; }

        public RecipesModel(IRecipeService recipeService) => _recipeService = recipeService;

        public void OnGet(int page)
        {
            const int defaultPageSize = 9;
            int pageSize = defaultPageSize;

            if (page <= 0)
                page = 1;

            int totalRecipes = _recipeService.GetTotalCount();
            TotalPages = CalculateTotalPages(totalRecipes, pageSize);

            if (page > TotalPages)
                page = TotalPages;

            CurrentPage = page;
            Recipes = _recipeService.GetPage(CurrentPage, pageSize);
        }

        private static int CalculateTotalPages(int totalItems, int pageSize)
        {
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            return totalPages > 0 ? totalPages : 1;
        }
    }
}