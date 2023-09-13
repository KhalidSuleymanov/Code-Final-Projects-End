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
    public class ReviewController : Controller
    {
        private readonly HimaraDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ReviewController(HimaraDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Search = search;
            var query = _context.Reviews.AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.Name.Contains(search));
            }
            return View(PaginatedList<Review>.Create(query, page, 5));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Review review)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (review.ImageFile == null)
            {
                ModelState.AddModelError("Image", "Image file is required");
                return View();
            }
            review.Image = FileManager.Save(review.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/ReviewImage");
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var existReview = _context.Reviews.Find(id);
            if (existReview == null)
            {
                return View("Error");
            }
            return View(existReview);
        }
        [HttpPost]
        public IActionResult Edit(Review review)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existReview = _context.Reviews.Find(review.Id);
            if (existReview == null)
            {
                return View("Error");
            }
            string removableImage = null;
            if (review.ImageFile != null)
            {
                removableImage = existReview.Image;
                existReview.Image = FileManager.Save(review.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/ReviewImage");
            }
            existReview.Description = review.Description;
            existReview.Name = review.Name;
            existReview.Adress = review.Adress;
            _context.SaveChanges();

            if (removableImage != null)
            {
                FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/ReviewImage", removableImage);
            }
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var existReview = _context.Reviews.Find(id);
            if (existReview == null)
            {
                return View("Error");
            }
            var removableImage = existReview.Image;
            _context.Reviews.Remove(existReview);
            _context.SaveChanges();
            FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/ReviewImage", removableImage);
            return RedirectToAction("index");
        }
    }
}
