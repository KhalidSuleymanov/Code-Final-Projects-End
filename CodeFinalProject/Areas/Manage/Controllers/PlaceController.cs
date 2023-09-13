using CodeFinalProject.Areas.Manage.ViewModels;
using CodeFinalProject.DAL;
using CodeFinalProject.Helpers;
using CodeFinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CodeFinalProject.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    [Area("manage")]
    public class PlaceController : Controller
    {
        private readonly HimaraDbContext _context;
        private readonly IWebHostEnvironment _env;
        public PlaceController(HimaraDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Search = search;
            var query = _context.Places.AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.Name.Contains(search));
            }
            return View(PaginatedList<Place>.Create(query, page, 5));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Place place)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (place.ImageFile == null)
            {
                ModelState.AddModelError("Image", "Image file is required");
                return View();
            }
            place.Image = FileManager.Save(place.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/PlaceImage");
            _context.Places.Add(place);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var existPlace = _context.Places.Find(id);
            if (existPlace == null)
            {
                return View("Error");
            }
            return View(existPlace);
        }

        [HttpPost]
        public IActionResult Edit(Place place)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existPlace = _context.Places.Find(place.Id);
            if (existPlace == null)
            {
                return View("Error");
            }
            string removableImage = null;
            if (place.ImageFile != null)
            {
                removableImage = existPlace.Image;
                existPlace.Image = FileManager.Save(place.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/PlaceImage");
            }
            existPlace.Name = place.Name;
            existPlace.Description1 = place.Description1;
            existPlace.Description2 = place.Description2;
            _context.SaveChanges();
            if (removableImage != null)
            {
                FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/PlaceImage", removableImage);
            }
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var existPlace = _context.Places.Find(id);
            if (existPlace == null)
            {
                return View("Error");
            }
            var removableImage = existPlace.Image;
            _context.Places.Remove(existPlace);
            _context.SaveChanges();
            FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/PlaceImage", removableImage);
            return RedirectToAction("index");
        }
    }
}
