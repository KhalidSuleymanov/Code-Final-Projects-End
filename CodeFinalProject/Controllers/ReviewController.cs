using CodeFinalProject.DAL;
using CodeFinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeFinalProject.Controllers
{
    public class ReviewController : Controller
    {
        private readonly HimaraDbContext _context;
        public ReviewController(HimaraDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM hovm = new HomeVM
            {
                Reviews=_context.Reviews.ToList(),
            };
            return View(hovm);
        }
    }
}
