using Domain;

namespace Repositories.Recipes.Items
{
    public interface IRecipeItemRepository<T>
    {
        IEnumerable<T> Find(Recipe recipe);
        void Add(Recipe recipe, T item);
        void Update(Recipe recipe, T oldItem, T newItem);
        void Delete(Recipe recipe, T item);
    }
}
