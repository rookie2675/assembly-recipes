using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Recipes;

namespace WebApp.Pages
{
    public class RecipeModel : PageModel
    {
        private readonly ILogger<RecipeModel> _logger;
        private readonly IRecipeService _recipeService;

        public Recipe? Recipe { get; private set; }

        [BindProperty]
        public long RecipeId { get; set; }

        public RecipeModel(ILogger<RecipeModel> logger, IRecipeService recipeService)
        {
            _logger = logger;
            _recipeService = recipeService;

        }

        public IActionResult OnGet(long? id)
        {
            if (id is null || id.Value == 0)
                return BadRequest("Invalid Recipe ID");

            Recipe = _recipeService.GetById(id.Value);

            if (Recipe == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPostDelete(Recipe recipe)
        {
            _logger.LogInformation($"OnPostDelete called with recipe: {recipe}");

            if (recipe is null || !recipe.Id.HasValue || recipe.Id.Value == 0)
            {
                _logger.LogWarning("Invalid Recipe");
                return BadRequest("Invalid Recipe");
            }

            _logger.LogInformation($"Deleting recipe with ID: {recipe.Id.Value}");
            _recipeService.Delete(recipe.Id.Value);

            return RedirectToPage("/Recipes");
        }
    }
}