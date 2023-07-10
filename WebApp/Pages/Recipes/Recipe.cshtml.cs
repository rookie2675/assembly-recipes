using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;

namespace WebApp.Pages.Recipes
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

            Recipe = _recipeService.Find(id.Value);

            if (Recipe == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPostDelete(long? id)
        {
            if (id is null || id.Value == 0)
                return BadRequest("Invalid Recipe ID");

            Recipe = _recipeService.Find(id.Value);

            if (Recipe == null)
                return NotFound();

            _recipeService.Delete(id.Value);

            return RedirectToPage("Index");
        }

        internal bool IsUserLoggedIn() => HttpContext.Session.GetInt32("UserId") is not null;
    }
}