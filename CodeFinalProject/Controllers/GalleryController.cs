using CodeFinalProject.DAL;
using CodeFinalProject.Models;
using CodeFinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeFinalProject.Controllers
{
    public class GalleryController : Controller
    {
        private readonly HimaraDbContext _context;

        public GalleryController(HimaraDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM vM = new HomeVM
            {
                Galleries = _context.Galleries.ToList(),
            };
            return View(vM);
        }
    }
}
