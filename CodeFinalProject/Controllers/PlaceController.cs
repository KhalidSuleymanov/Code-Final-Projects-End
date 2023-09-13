using CodeFinalProject.DAL;
using CodeFinalProject.Models;
using CodeFinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeFinalProject.Controllers
{
    public class PlaceController : Controller
    {
        private readonly HimaraDbContext _context;

        public PlaceController(HimaraDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            PlaceDetailViewModel placeVM = new PlaceDetailViewModel
            {
                Places = _context.Places.Take(8).ToList()
            };
            return View(placeVM);
        }

        public IActionResult PlaceDetail(int id)
        {
            var place = _context.Places.FirstOrDefault(x => x.Id == id);
            return View(place);
        }
    }
}
