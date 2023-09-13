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
    public class GalleryController : Controller
    {
        private readonly HimaraDbContext _context;
        private readonly IWebHostEnvironment _env;
        public GalleryController(HimaraDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Search = search;
            var query = _context.Galleries.AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.Title.Contains(search));
            }
            return View(PaginatedList<Gallery>.Create(query, page, 5));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Gallery gallery)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (gallery.ImageFile == null)
            {
                ModelState.AddModelError("Image", "Image file is required");
                return View();
            }
            gallery.Image = FileManager.Save(gallery.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/GalleryImage");
            _context.Galleries.Add(gallery);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var existGallery = _context.Galleries.Find(id);
            if (existGallery == null)
            {
                return View("Error");
            }
            return View(existGallery);
        }
        [HttpPost]
        public IActionResult Edit(Gallery gallery)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existGallery = _context.Galleries.Find(gallery.Id);
            if (existGallery == null)
            {
                return View("Error");
            }
            string removableImage = null;
            if (gallery.ImageFile != null)
            {
                removableImage = existGallery.Image;
                existGallery.Image = FileManager.Save(gallery.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/GalleryImage");
            }
            existGallery.Title = gallery.Title;
            existGallery.ModalClick = gallery.ModalClick;
            existGallery.ModalNumber = gallery.ModalNumber;
            _context.SaveChanges();
            if (removableImage != null)
            {
                FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/GalleryImage", removableImage);
            }
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var existGallery = _context.Galleries.Find(id);
            if (existGallery == null)
            {
                return View("Error");
            }
            var removableImage = existGallery.Image;
            _context.Galleries.Remove(existGallery);
            _context.SaveChanges();
            FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/GalleryImage", removableImage);
            return RedirectToAction("index");
        }
    }
}
