using CodeFinalProject.Areas.Manage.ViewModels;
using CodeFinalProject.DAL;
using CodeFinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CodeFinalProject.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    [Area("manage")]
    public class RestaurantController : Controller
    {
        private readonly HimaraDbContext _context;
        public RestaurantController(HimaraDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Search = search;
            var query = _context.Restaurants.AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.Name.Contains(search));
            }
            return View(PaginatedList<Restaurant>.Create(query, page, 5));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Restaurant rest)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Restaurants.Add(rest);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var existRestaurant = _context.Restaurants.Find(id);
            if (existRestaurant == null)
            {
                return View("Error");
            }
            return View(existRestaurant);
        }

        [HttpPost]
        public IActionResult Edit(Restaurant rest)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existRest = _context.Restaurants.Find(rest.Id);
            if (existRest == null)
            {
                return View("Error");
            }
            existRest.Name = rest.Name;
            existRest.Description1 = rest.Description1;
            existRest.Description2 = rest.Description2;
            existRest.Description3 = rest.Description3;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            var existRest = _context.Restaurants.Find(id);
            if (existRest == null)
            {
                return View("Error");
            }
            _context.Restaurants.Remove(existRest);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
