namespace CodeFinalProject.Models
{
    public class MealIngredient
    {
        public Meal Meal { get; set; }

        public Ingredient Ingredient { get; set; }

        public int MealId { get; set; }

        public int IngredientId { get; set; }
    }
}
