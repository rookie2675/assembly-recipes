using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Domain;
using Services.Contracts;

namespace WebApp.Pages
{
    public class CreateRecipeModel : PageModel
    {
        private readonly IRecipeService _recipeService;

        [Required(ErrorMessage = "Ingredients field is required.")]
        public List<string> Ingredients { get; set; }

        [BindProperty]
        public Recipe? Recipe { get; set; }

        public CreateRecipeModel(IRecipeService recipeService)
        {
            _recipeService = recipeService;
            Ingredients = new List<string>();
        }

        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Recipe = _recipeService.GetById(id.Value);

                if (Recipe is null)
                    return NotFound();
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
                Recipe = new Recipe();
                _recipeService.Add(Recipe);
            }

            return RedirectToPage("/Recipes/Index");
        }
    }
}
