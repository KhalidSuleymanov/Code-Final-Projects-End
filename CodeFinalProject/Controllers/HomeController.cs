using CodeFinalProject.DAL;
using CodeFinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace CodeFinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly HimaraDbContext _context;
        public HomeController(HimaraDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Features = _context.Features.Take(3).ToList(),
                Staffs = _context.Staffs.Where(x => x.Title == "Staff").Take(4).ToList(),
                Galleries = _context.Galleries.Take(4).ToList(),
                Meals = _context.Meals.Take(4).ToList(),
                Places = _context.Places.Take(4).ToList(),
            };
            return View(homeVM);
        }
    }
}
