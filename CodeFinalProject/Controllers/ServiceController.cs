using CodeFinalProject.DAL;
using CodeFinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeFinalProject.Controllers
{
    public class ServiceController : Controller
    {
        private readonly HimaraDbContext _context;
        public ServiceController(HimaraDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Services = _context.Services.ToList(),
            };
            return View(homeVM);
        }
    }
}
