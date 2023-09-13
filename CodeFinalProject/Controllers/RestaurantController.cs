using CodeFinalProject.DAL;
using CodeFinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeFinalProject.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly HimaraDbContext _context;
        public RestaurantController(HimaraDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            RestaurantViewModel restaurant = new RestaurantViewModel
            {
                Meals = _context.Meals.ToList(),
                RestaurantModals = _context.RestaurantModals.ToList(),
                Chefs= _context.Chefs.ToList(),
            };
            return View(restaurant);
        }
    }
}
