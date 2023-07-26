using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Recipes;

namespace WebApp.Pages
{
    public class UpdateRecipeModel : PageModel
    {
        private readonly IRecipeService _recipeService;
        private readonly ILogger<UpdateRecipeModel> _logger;

        [BindProperty]
        public Recipe? Recipe { get; set; }

        public UpdateRecipeModel(ILogger<UpdateRecipeModel> logger, IRecipeService recipeService)
        {
            _logger = logger;
            _recipeService = recipeService;
        }

        public IActionResult OnGet(int? id)
        {
            if (!id.HasValue)
                return NotFound();

            Recipe = _recipeService.GetById(id.Value);

            if (Recipe is null)
            {
                _logger.LogInformation($"No recipe was found for ID: {id}");
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _recipeService.Update(Recipe);

            return RedirectToPage("/Recipes");
        }
    }
}