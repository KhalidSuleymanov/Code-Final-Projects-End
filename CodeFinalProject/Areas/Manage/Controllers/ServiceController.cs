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
    public class ServiceController : Controller
    {
        private readonly HimaraDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ServiceController(HimaraDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Search = search;
            var query = _context.Services.AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.Name.Contains(search));
            }
            return View(PaginatedList<Service>.Create(query, page, 5));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Service service)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Services.Add(service);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var existService = _context.Services.Find(id);
            if (existService == null)
            {
                return View("Error");
            }
            return View(existService);
        }

        [HttpPost]
        public IActionResult Edit(Service service)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existService = _context.Services.Find(service.Id);
            if (existService == null)
            {
                return View("Error");
            }
            existService.Name = service.Name;
            existService.IconImage = service.IconImage;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            var existService = _context.Services.Find(id);
            if (existService == null)
            {
                return View("Error");
            }
            _context.Services.Remove(existService);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
