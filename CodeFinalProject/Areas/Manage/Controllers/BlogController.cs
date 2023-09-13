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
    public class BlogController : Controller
    {
        private readonly HimaraDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BlogController(HimaraDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Search = search;
            var query = _context.Blogs.AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.Name.Contains(search));
            }
            return View(PaginatedList<Blog>.Create(query, page, 5));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (blog.ImageFile == null)
            {
                ModelState.AddModelError("Image", "Image file is required");
                return View();
            }
            blog.Image = FileManager.Save(blog.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/BlogImage");
            _context.Blogs.Add(blog);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var existBlog = _context.Blogs.Find(id);
            if (existBlog == null)
            {
                return View("Error");
            }
            return View(existBlog);
        }
        [HttpPost]
        public IActionResult Edit(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existBlog = _context.Blogs.Find(blog.Id);
            if (existBlog == null)
            {
                return View("Error");
            }
            string removableImage = null;

            if (blog.ImageFile != null)
            {
                removableImage = existBlog.Image;
                existBlog.Image = FileManager.Save(blog.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/BlogImage");
            }
            existBlog.Description = blog.Description;
            existBlog.Name = blog.Name;
            _context.SaveChanges();
            if (removableImage != null)
            {
                FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/BlogImage", removableImage);
            }
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            var existBlog = _context.Blogs.Find(id);
            if (existBlog == null)
            {
                return View("Error");
            }
            var removableImage = existBlog.Image;
            _context.Blogs.Remove(existBlog);
            _context.SaveChanges();
            FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/BlogImage", removableImage);
            return RedirectToAction("index");
        }
    }
}
