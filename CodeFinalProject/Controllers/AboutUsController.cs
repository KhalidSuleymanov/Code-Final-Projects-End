using CodeFinalProject.DAL;
using CodeFinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeFinalProject.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly HimaraDbContext _context;
        public AboutUsController(HimaraDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            AboutViewModel abot = new AboutViewModel
            {
                Features = _context.Features.Take(8).ToList(),
                Staffs = _context.Staffs.Where(x => x.Title == "Staff").Take(4).ToList(),
                AboutServices = _context.AboutServices.Take(4).ToList(),
            };
            return View(abot);
        }
    }
}
