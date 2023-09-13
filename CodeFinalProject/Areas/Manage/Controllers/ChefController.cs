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
    public class ChefController : Controller
    {
        private readonly HimaraDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ChefController(HimaraDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Search = search;
            var query = _context.Chefs.AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.Name.Contains(search));
            }
            return View(PaginatedList<Chef>.Create(query, page, 5));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Chef chef)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (chef.ImageFile == null)
            {
                ModelState.AddModelError("Image", "Image file is required");
                return View();
            }
            chef.Image = FileManager.Save(chef.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/ChefImage");
            _context.Chefs.Add(chef);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var existChef = _context.Chefs.Find(id);
            if (existChef == null)
            {
                return View("Error");
            }
            return View(existChef);
        }
        [HttpPost]
        public IActionResult Edit(Chef chef)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existChef = _context.Chefs.Find(chef.Id);
            if (existChef == null)
            {
                return View("Error");
            }
            string removableImage = null;
            if (chef.ImageFile != null)
            {
                removableImage = existChef.Image;
                existChef.Image = FileManager.Save(chef.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/ChefImage");
            }
            existChef.Description = chef.Description;
            existChef.Name = chef.Name;
            existChef.Position = chef.Position;
            _context.SaveChanges();

            if (removableImage != null)
            {
                FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/ChefImage", removableImage);
            }
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var existChef = _context.Chefs.Find(id);
            if (existChef == null)
            {
                return View("Error");
            }
            var removableImage = existChef.Image;
            _context.Chefs.Remove(existChef);
            _context.SaveChanges();
            FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/ChefImage", removableImage);
            return RedirectToAction("index");
        }
    }
}
