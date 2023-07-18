using System.Text;

namespace Domain
{
    public class Recipe
    {
        private readonly List<Step> _steps;
        private readonly List<Ingredient> _ingredients;

        public long? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? ShortDescription { get; set; }

        public string? ImageURL { get; set; }

        public IReadOnlyList<Step> Steps => _steps;

        public IReadOnlyList<Ingredient> Ingredients => _ingredients;

        public Recipe()
        {
            _steps = new();
            _ingredients = new();
        }

        public void AddIngredient(Ingredient ingredient)
        {
            if (ingredient is null)
                throw new ArgumentNullException(nameof(ingredient));

            _ingredients.Add(ingredient);
        }

        public void AddStep(Step step)
        {
            if (step is null) 
                throw new ArgumentNullException(nameof(step));

            _steps.Add(step);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Recipe Id: {Id}");
            stringBuilder.AppendLine($"Name: {Name}");
            stringBuilder.AppendLine($"Description: {Description}");
            stringBuilder.AppendLine($"Short Description: {ShortDescription}");
            stringBuilder.AppendLine($"Image URL: {ImageURL}");

            stringBuilder.AppendLine("Ingredients:");
            foreach (var ingredient in Ingredients)
                stringBuilder.AppendLine($"- {ingredient}");

            stringBuilder.AppendLine("Steps:");
            foreach (var step in Steps)
                stringBuilder.AppendLine($"- {step}");

            return stringBuilder.ToString();
        }
    }
}