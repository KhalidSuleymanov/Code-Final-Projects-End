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
    public class StaffController : Controller
    {
        private readonly HimaraDbContext _context;
        private readonly IWebHostEnvironment _env;
        public StaffController(HimaraDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Search = search;
            var query = _context.Staffs.AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.Name.Contains(search));
            }
            return View(PaginatedList<Staff>.Create(query, page, 5));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (staff.ImageFile == null)
            {
                ModelState.AddModelError("Image", "Image file is required");
                return View();
            }
            staff.Image = FileManager.Save(staff.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/StaffImage");
            _context.Staffs.Add(staff);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var existStaff = _context.Staffs.Find(id);
            if (existStaff == null)
            {
                return View("Error");
            }
            return View(existStaff);
        }

        [HttpPost]
        public IActionResult Edit(Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existStaff = _context.Staffs.Find(staff.Id);
            if (existStaff == null)
            {
                return View("Error");
            }
            string removableImage = null;
            if (staff.ImageFile != null)
            {
                removableImage = existStaff.Image;
                existStaff.Image = FileManager.Save(staff.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/StaffImage");
            }
            existStaff.Description = staff.Description;
            existStaff.Name = staff.Name;
            existStaff.Position = staff.Position;
            existStaff.Title = staff.Title;
            _context.SaveChanges();

            if (removableImage != null)
            {
                FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/StaffImage", removableImage);
            }
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            var existStaff = _context.Staffs.Find(id);
            if (existStaff == null)
            {
                return View("Error");
            }
            var removableImage = existStaff.Image;
            _context.Staffs.Remove(existStaff);
            _context.SaveChanges();
            FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/StaffImage", removableImage);
            return RedirectToAction("index");
        }
    }
}
