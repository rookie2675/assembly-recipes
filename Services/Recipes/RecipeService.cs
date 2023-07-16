using Domain;
using Repositories.Recipes;
using Repositories.Recipes.Items.Ingredients;
using Repositories.Recipes.Items.Steps;

namespace Services.Recipes
{
    public class RecipeService : IRecipeService
    {
        private readonly IStepsRepository _stepsRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngredientRepository _ingredientRepository;

        public RecipeService(IStepsRepository stepsRepository, IRecipeRepository recipeRepository, IIngredientRepository ingredientRepository)
        {
            _stepsRepository = stepsRepository;
            _recipeRepository = recipeRepository;
            _ingredientRepository = ingredientRepository;

        }

        public Recipe? GetById(long id)
        {
            Recipe? recipe = _recipeRepository.FindById(id);

            if (recipe is not null)
            {
                foreach (var step in _stepsRepository.FindAllByRecipe(recipe))
                    recipe.AddStep(step);

                foreach (var ingredient in _ingredientRepository.FindAllByRecipe(recipe))
                    recipe.AddIngredient(ingredient);
            }

            return recipe;
        }

        public List<Recipe> GetAll() => _recipeRepository.FindAll();

        public IEnumerable<Recipe> GetPage(int page, int size) => _recipeRepository.FindPage(page, size);

        public Recipe Add(Recipe recipe)
        {
            Recipe createdRecipe = _recipeRepository.Add(recipe);

            foreach (var ingredient in recipe.Ingredients)
                _ingredientRepository.Add(recipe, ingredient);

            foreach (var step in recipe.Steps)
                _stepsRepository.Add(recipe, step);

            return createdRecipe;
        }

        public Recipe Update(Recipe recipe) 
        {
            Recipe updatedRecipe = _recipeRepository.Update(recipe);

            foreach (var ingredient in recipe.Ingredients)
                _ingredientRepository.Update(recipe, ingredient);

            foreach (var step in recipe.Steps)
                _stepsRepository.Update(recipe, step);

            return updatedRecipe;
        }

        public Recipe Delete(long id) => _recipeRepository.Delete(id);

        public int GetTotalCount() => _recipeRepository.GetTotalCount();
    }
}