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
    public class RestaurantModalController : Controller
    {
        private readonly HimaraDbContext _context;
        private readonly IWebHostEnvironment _env;
        public RestaurantModalController(HimaraDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Search = search;
            var query = _context.RestaurantModals.AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.Name.Contains(search));
            }
            return View(PaginatedList<RestaurantModal>.Create(query, page, 5));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(RestaurantModal restmod)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (restmod.ImageFile == null)
            {
                ModelState.AddModelError("Image", "Image file is required");
                return View();
            }
            restmod.Image = FileManager.Save(restmod.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/RestaurantModalImage");
            _context.RestaurantModals.Add(restmod);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var existModal = _context.RestaurantModals.Find(id);
            if (existModal == null)
            {
                return View("Error");
            }
            return View(existModal);
        }

        [HttpPost]
        public IActionResult Edit(RestaurantModal modal)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existModal = _context.RestaurantModals.Find(modal.Id);
            if (existModal == null)
            {
                return View("Error");
            }
            string removableImage = null;
            if (modal.ImageFile != null)
            {
                removableImage = existModal.Image;
                existModal.Image = FileManager.Save(modal.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/RestaurantModalImage");
            }
            existModal.Name = modal.Name;
            _context.SaveChanges();
            if (removableImage != null)
            {
                FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/RestaurantModalImage", removableImage);
            }
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var existModal = _context.RestaurantModals.Find(id);
            if (existModal == null)
            {
                return View("Error");
            }
            var removableImage = existModal.Image;
            _context.RestaurantModals.Remove(existModal);
            _context.SaveChanges();
            FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/RestaurantModalImage", removableImage);
            return RedirectToAction("index");
        }
    }
}
