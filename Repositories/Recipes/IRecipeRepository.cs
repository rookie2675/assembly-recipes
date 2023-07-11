using Domain;
using Repositories.Contracts;

namespace Repositories.Recipes;

public interface IRecipeRepository : IRepository<Recipe>, IPagedRepository<Recipe> { }