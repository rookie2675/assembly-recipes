using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;

namespace WebApp.Pages
{
    public class RecipeModel : PageModel
    {
        private readonly IRecipeService _recipeService;

        public Recipe? Recipe { get; private set; }

        public RecipeModel(IRecipeService recipeService) => _recipeService = recipeService;

        public IActionResult OnGet(long? id)
        {
            if (id is null || id.Value == 0)
                return BadRequest("Invalid Recipe ID");

            Recipe = _recipeService.GetById(id.Value);

            if (Recipe == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPostDelete(long? id)
        {
            if (id is null || id.Value == 0)
                return BadRequest("Invalid Recipe ID");

            if (Recipe == null)
                return NotFound();

            if (Recipe.Id is not null)
            {

            }

            _recipeService.Delete(Recipe.Id.Value);

            return RedirectToPage("/Recipes/Recipes");
        }
    }
}