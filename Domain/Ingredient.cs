namespace Domain
{
    public class Ingredient
    {
        public required string Name { get; set; }
        public required double Quantity { get; set; }
        public required string Unit { get; set; }

        public override string ToString() => $"{Quantity} {Unit} of {Name}";
    }
}