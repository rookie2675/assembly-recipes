using Domain;

using Repositories.Contracts;

using Services.Contracts;

namespace Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository) => _recipeRepository = recipeRepository;

        public Recipe Find(long id) => _recipeRepository.Find(id);

        public List<Recipe> Find() => _recipeRepository.Find();

        public Recipe Add(Recipe recipe) => _recipeRepository.Add(recipe);

        public Recipe Update(Recipe recipe) => _recipeRepository.Update(recipe);

        public Recipe Delete(long id) => _recipeRepository.Delete(id);
    }
}