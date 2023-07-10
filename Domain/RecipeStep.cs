using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class RecipeStep
    {
        [Required(ErrorMessage = "Step number is required.")]
        [Range(1, 50, ErrorMessage = "Step number must be between 1 and 50.")]
        public required int StepNumber { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 100 characters.")]
        public required string Description { get; set; }
    }
}