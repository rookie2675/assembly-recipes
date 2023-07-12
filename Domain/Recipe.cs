namespace Domain
{
    public class Recipe
    {
        private readonly List<string> _steps;
        private readonly List<string> _ingredients;

        public long? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? ShortDescription { get; set; }

        public string? ImageURL { get; set; }

        public IReadOnlyList<string> Steps => _steps;

        public IReadOnlyList<string> Ingredients => _ingredients;

        public Recipe()
        {
            _steps = new();
            _ingredients = new();
        }


        public void AddIngredient(string ingredient)
        {
            if (string.IsNullOrWhiteSpace(ingredient)) throw new ArgumentException("Ingredient cannot be null or empty.", nameof(ingredient));

            _ingredients.Add(ingredient);
        }

        public void AddStep(string step)
        {
            if (string.IsNullOrWhiteSpace(step)) throw new ArgumentException("Step cannot be null or empty.", nameof(step));

            _steps.Add(step);
        }
    }
}