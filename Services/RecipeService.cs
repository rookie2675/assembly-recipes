using Domain;
using PagedList;
using Repositories.Recipes;
using Services.Contracts;

namespace Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository) => _recipeRepository = recipeRepository;

        public Recipe GetById(long id) => _recipeRepository.FindById(id);

        public List<Recipe> GetAll() => _recipeRepository.FindAll();

        public IEnumerable<Recipe> GetPage(int page, int size) => _recipeRepository.FindPage(page, size);

        public Recipe Add(Recipe recipe) => _recipeRepository.Add(recipe);

        public Recipe Update(Recipe recipe) => _recipeRepository.Update(recipe);

        public Recipe Delete(long id) => _recipeRepository.Delete(id);

        public int GetTotalCount() => _recipeRepository.GetTotalCount();
    }
}