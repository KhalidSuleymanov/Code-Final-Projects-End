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
    public class AwardController : Controller
    {
        private readonly HimaraDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AwardController(HimaraDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Search = search;
            var query = _context.Awards.AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.Name.Contains(search));
            }
            return View(PaginatedList<Award>.Create(query, page, 5));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Award award)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (award.ImageFile == null)
            {
                ModelState.AddModelError("Image", "Image file is required");
                return View();
            }
            award.Image = FileManager.Save(award.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/AwardImage");
            _context.Awards.Add(award);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var existAward = _context.Awards.Find(id);
            if (existAward == null)
            {
                return View("Error");
            }
            return View(existAward);
        }
        [HttpPost]
        public IActionResult Edit(Award award)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existAward = _context.Awards.Find(award.Id);
            if (existAward == null)
            {
                return View("Error");
            }
            string removableImage = null;
            if (award.ImageFile != null)
            {
                removableImage = existAward.Image;
                existAward.Image = FileManager.Save(award.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/AwardImage");
            }
            existAward.Name = award.Name;
            _context.SaveChanges();

            if (removableImage != null)
            {
                FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/AwardImage", removableImage);
            }
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var existAward = _context.Awards.Find(id);
            if (existAward == null)
            {
                return View("Error");
            }
            var removableImage = existAward.Image;
            _context.Awards.Remove(existAward);
            _context.SaveChanges();
            FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/AwardImage", removableImage);
            return RedirectToAction("index");
        }
    }
}
