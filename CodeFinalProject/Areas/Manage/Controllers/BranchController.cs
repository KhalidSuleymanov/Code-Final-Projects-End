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
    public class BranchController : Controller
    {
        private readonly HimaraDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BranchController(HimaraDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Search = search;
            var query = _context.Branchs.AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.Name.Contains(search));
            }
            return View(PaginatedList<Branch>.Create(query, page, 5));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Branch branch)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (branch.ImageFile == null)
            {
                ModelState.AddModelError("Image", "Image file is required");
                return View();
            }
            branch.Image = FileManager.Save(branch.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/BranchImage");
            _context.Branchs.Add(branch);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var existBranch = _context.Branchs.Find(id);
            if (existBranch == null)
            {
                return View("Error");
            }
            return View(existBranch);
        }
        [HttpPost]
        public IActionResult Edit(Branch branch)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existBranch = _context.Branchs.Find(branch.Id);
            if (existBranch == null)
            {
                return View("Error");
            }
            string removableImage = null;
            if (branch.ImageFile != null)
            {
                removableImage = existBranch.Image;
                existBranch.Image = FileManager.Save(branch.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/BranchImage");
            }
            existBranch.Name = branch.Name;
            existBranch.Address = branch.Address;
            _context.SaveChanges();
            if (removableImage != null)
            {
                FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/BranchImage", removableImage);
            }
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var existBranch = _context.Branchs.Find(id);
            if (existBranch == null)
            {
                return View("Error");
            }
            var removableImage = existBranch.Image;
            _context.Branchs.Remove(existBranch);
            _context.SaveChanges();
            FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/BranchImage", removableImage);
            return RedirectToAction("index");
        }
    }
}
