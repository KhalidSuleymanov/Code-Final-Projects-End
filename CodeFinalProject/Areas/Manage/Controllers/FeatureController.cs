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
    public class FeatureController : Controller
    {
        private readonly HimaraDbContext _context;
        private readonly IWebHostEnvironment _env;
        public FeatureController(HimaraDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Search = search;
            var query = _context.Features.AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.Name.Contains(search));
            }
            return View(PaginatedList<Feature>.Create(query, page, 5));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Feature feature)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Features.Add(feature);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var existFeat = _context.Features.Find(id);
            if (existFeat == null)
            {
                return View("Error");
            }
            return View(existFeat);
        }

        [HttpPost]
        public IActionResult Edit(Feature feature)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existFeature = _context.Features.Find(feature.Id);
            if (existFeature == null)
            {
                return View("Error");
            }
            existFeature.Name = feature.Name;
            existFeature.IconImage = feature.IconImage;
            existFeature.FeatureCount = feature.FeatureCount;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            var existFeature = _context.Features.Find(id);
            if (existFeature == null)
            {
                return View("Error");
            }
            _context.Features.Remove(existFeature);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
