using Domain;

namespace Repositories.Recipes.Items
{
    public interface IRecipeItemRepository<T>
    {
        ICollection<T> FindAllByRecipe(Recipe recipe);
        void Add(Recipe recipe, T item);
        void Update(Recipe recipe, T item);
        void Delete(Recipe recipe, T item);
    }
}
