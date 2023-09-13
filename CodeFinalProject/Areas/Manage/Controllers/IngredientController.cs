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
    public class IngredientController : Controller
    {
        private readonly HimaraDbContext _context;
        private readonly IWebHostEnvironment _env;
        public IngredientController(HimaraDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Search = search;
            var query = _context.Ingredients.AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.Name.Contains(search));
            }
            return View(PaginatedList<Ingredient>.Create(query, page, 5));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Ingredients.Add(ingredient);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var existIngredient = _context.Ingredients.Find(id);
            if (existIngredient == null)
            {
                return View("Error");
            }
            return View(existIngredient);
        }
        [HttpPost]
        public IActionResult Edit(Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existIngredient = _context.Ingredients.Find(ingredient.Id);
            if (existIngredient == null)
            {
                return View("Error");
            }
            existIngredient.Name = ingredient.Name;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var existIngredient = _context.Ingredients.Find(id);
            if (existIngredient == null)
            {
                return View("Error");
            }
            _context.Ingredients.Remove(existIngredient);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
