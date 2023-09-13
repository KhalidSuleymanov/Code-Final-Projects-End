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
    public class MealController : Controller
    {
        private readonly HimaraDbContext _context;
        private readonly IWebHostEnvironment _env;
        public MealController(HimaraDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Search = search;
            var query = _context.Meals.AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.Name.Contains(search));
            }
            return View(PaginatedList<Meal>.Create(query, page, 5));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (meal.ImageFile == null)
            {
                ModelState.AddModelError("Image", "Image file is required");
                return View();
            }
            meal.Image = FileManager.Save(meal.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/MealImage");
            _context.Meals.Add(meal);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var existMeal = _context.Meals.Find(id);
            if (existMeal == null)
            {
                return View("Error");
            }
            return View(existMeal);
        }
        [HttpPost]
        public IActionResult Edit(Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existMeal = _context.Meals.Find(meal.Id);
            if (existMeal == null)
            {
                return View("Error");
            }
            string removableImage = null;
            if (meal.ImageFile != null)
            {
                removableImage = existMeal.Image;
                existMeal.Image = FileManager.Save(meal.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/MealImage");
            }
            existMeal.Name = meal.Name;
            existMeal.Price = meal.Price;
            existMeal.Description = meal.Description;
            _context.SaveChanges();

            if (removableImage != null)
            {
                FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/MealImage", removableImage);
            }
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var existMeal = _context.Meals.Find(id);
            if (existMeal == null)
            {
                return View("Error");
            }
            var removableImage = existMeal.Image;
            _context.Meals.Remove(existMeal);
            _context.SaveChanges();
            FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/MealImage", removableImage);
            return RedirectToAction("index");
        }
    }
}
