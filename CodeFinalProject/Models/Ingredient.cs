using System.ComponentModel.DataAnnotations;

namespace CodeFinalProject.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public List<MealIngredient> MealIngredients { get; set; }
    }
}
