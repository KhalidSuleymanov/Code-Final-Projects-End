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
    public class RecentPostController : Controller
    {
        private readonly HimaraDbContext _context;
        public RecentPostController(HimaraDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Search = search;
            var query = _context.RecentPosts.AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.Title.Contains(search));
            }
            return View(PaginatedList<RecentPost>.Create(query, page, 5));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(RecentPost post)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.RecentPosts.Add(post);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var existPost = _context.RecentPosts.Find(id);
            if (existPost == null)
            {
                return View("Error");
            }
            return View(existPost);
        }

        [HttpPost]
        public IActionResult Edit(RecentPost post)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existPost = _context.RecentPosts.Find(post.Id);
            if (existPost == null)
            {
                return View("Error");
            }
            existPost.Title = post.Title;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            var existPost = _context.RecentPosts.Find(id);
            if (existPost == null)
            {
                return View("Error");
            }
            _context.RecentPosts.Remove(existPost);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
