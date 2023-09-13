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
    public class SpaServiceController : Controller
    {
        private readonly HimaraDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SpaServiceController(HimaraDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Search = search;
            var query = _context.SpaServices.AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.Name.Contains(search));
            }
            return View(PaginatedList<SpaService>.Create(query, page, 5));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SpaService spaService)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.SpaServices.Add(spaService);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var existService = _context.SpaServices.Find(id);
            if (existService == null)
            {
                return View("Error");
            }
            return View(existService);
        }
        [HttpPost]
        public IActionResult Edit(SpaService spaService)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existService = _context.SpaServices.Find(spaService.Id);
            if (existService == null)
            {
                return View("Error");
            }
            existService.Name = spaService.Name;
            existService.IconImage = spaService.IconImage;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var existService = _context.SpaServices.Find(id);
            if (existService == null)
            {
                return View("Error");
            }
            _context.SpaServices.Remove(existService);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
