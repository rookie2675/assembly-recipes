using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Recipes;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Pages
{
    public class CreateRecipeModel : PageModel
    {
        private readonly IRecipeService _recipeService;
        private readonly ILogger<CreateRecipeModel> _logger;

        [Required(ErrorMessage = "Ingredients field is required.")]
        public List<string> Ingredients { get; set; }

        [BindProperty]
        public Recipe? Recipe { get; set; }

        public CreateRecipeModel(ILogger<CreateRecipeModel> logger, IRecipeService recipeService)
        {
            _logger = logger;
            _recipeService = recipeService;
            Ingredients = new List<string>();
        }

        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Recipe = _recipeService.GetById(id.Value);

                if (Recipe is null)
                {
                    _logger.LogInformation($"No recipe was found for ID: {id}");
                    return NotFound();
                }
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (Recipe is not null && Recipe.Id > 0)
            {
                _recipeService.Update(Recipe);
            }

            else
            {
                if (Recipe is not null)
                {
                    _logger.LogInformation($"Request received to add recipe: { Recipe }");
                    _recipeService.Add(Recipe);
                }
            }

            return RedirectToPage("/Recipes");
        }
    }
}
