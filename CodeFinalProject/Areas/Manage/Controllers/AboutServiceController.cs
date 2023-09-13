using CodeFinalProject.Areas.Manage.ViewModels;
using CodeFinalProject.DAL;
using CodeFinalProject.Helpers;
using CodeFinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeFinalProject.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    [Area("manage")]
    public class AboutServiceController : Controller
    {
        private readonly HimaraDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AboutServiceController(HimaraDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Search = search;
            var query = _context.AboutServices.AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.Name.Contains(search));
            }
            return View(PaginatedList<AboutService>.Create(query, page, 5));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AboutService aboutService)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (aboutService.ImageFile == null)
            {
                ModelState.AddModelError("Image", "Image file is required");
                return View();
            }
            aboutService.Image = FileManager.Save(aboutService.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/AboutServiceImage");
            _context.AboutServices.Add(aboutService);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var existAbServices = _context.AboutServices.Find(id);
            if (existAbServices == null)
            {
                return View("Error");
            }
            return View(existAbServices);
        }
        [HttpPost]
        public IActionResult Edit(AboutService aboutService)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existAbServices = _context.AboutServices.Find(aboutService.Id);
            if (existAbServices == null)
            {
                return View("Error");
            }
            string removableImage = null;
            if (aboutService.ImageFile != null)
            {
                removableImage = existAbServices.Image;
                existAbServices.Image = FileManager.Save(aboutService.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/AboutServiceImage");
            }
            existAbServices.Name = aboutService.Name;
            _context.SaveChanges();
            if (removableImage != null)
            {
                FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/AboutServiceImage", removableImage);
            }
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var existServ = _context.AboutServices.Find(id);
            if (existServ == null)
            {
                return View("Error");
            }
            var removableImage = existServ.Image;
            _context.AboutServices.Remove(existServ);
            _context.SaveChanges();
            FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/AboutServiceImage", removableImage);
            return RedirectToAction("index");
        }
    }
}
