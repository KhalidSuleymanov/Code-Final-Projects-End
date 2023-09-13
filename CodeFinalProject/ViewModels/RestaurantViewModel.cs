using CodeFinalProject.Models;

namespace CodeFinalProject.ViewModels
{
    public class RestaurantViewModel
    {
        public List<Meal> Meals { get; set; }
        public List<RestaurantModal> RestaurantModals { get; set; }
        public List<Chef> Chefs { get; set; }
    }
}
