namespace Domain
{
    public class Recipe
    {
        public short? Id { get; init; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public required string ShortDescription { get; set; }

        public required string ImageURL { get; set; }

        public List<string> Steps { get; init; }

        public List<string> Ingredients { get; init; }

        public Recipe()
        {
            Ingredients = new();
            Steps = new();
        }

        public void AddIngredient(string ingredients) => Ingredients.Add(ingredients);

        public void AddStep(string step) => Steps.Add(step);
    }
}
