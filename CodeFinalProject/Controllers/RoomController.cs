using CodeFinalProject.DAL;
using CodeFinalProject.Models;
using CodeFinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace CodeFinalProject.Controllers
{
    public class RoomController : Controller
    {
        private readonly HimaraDbContext _context;
        public RoomController(HimaraDbContext context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    return View(_context.Rooms.ToList());
        //}
        //[HttpPost]'

        [HttpGet]
        public IActionResult Index(List<string>? name , int? elderCount , int? childCount, decimal? minPrice = null, decimal? maxPrice = null)
        {
           var query = _context.Rooms.AsQueryable();

            ShopViewModel vm = new ShopViewModel();

            vm.MinPrice = (int)query.Min(x => x.Price);
            vm.MaxPrice = (int)query.Max(x => x.Price);


            if (minPrice != null && maxPrice != null)
                query = query.Where(x => (int)x.Price >= (int)minPrice && (int)x.Price <= (int)maxPrice);

            if (name.Count>0)
                query = query.Where(x => name.Contains(x.Name));

            if (elderCount != null)
                query = query.Where(x => x.ElderCount == elderCount);

            if (childCount != null)
                query = query.Where(x => x.ChildCount == childCount);

            vm.Rooms = query.ToList();
            vm.Names = _context.Rooms.ToList();
            vm.SelectedName = name;
            vm.SelectedElderCount = elderCount;
            vm.SelectedChilCount= childCount;
            vm.SelectedMinPrice = (int)(minPrice == null ? vm.MinPrice : (decimal)minPrice);
            vm.SelectedMaxPrice = (int)(maxPrice == null ? vm.MaxPrice : (decimal)maxPrice);
            return View(vm);
        }

     
        public IActionResult Detail(int id)
        {
            var vm = _getRoomDetail(id);
            if (vm.Room == null)
            {
                return View("Error");
            }
            return View(vm);
        }
        [Authorize(Roles = "Member")]
        [HttpPost]
        public IActionResult RoomReview(RoomReview review)
        {
            if (!ModelState.IsValid)
            {
                var vm = _getRoomDetail(review.RoomId);
                vm.Review = review;
                return View("Detail", vm);
            }
            Room room = _context.Rooms.Include(x => x.RoomReviews).FirstOrDefault(x => x.Id == review.RoomId);
            if (room == null)
            {
                return View();
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null)
            {
                return View("Login");
            }
            review.AppUserId = userId;
            review.CreatedAt = DateTime.UtcNow.AddHours(4);
            room.RoomReviews.Add(review);
            room.Rate = (byte)Math.Ceiling(room.RoomReviews.Average(x => x.Rate));
            _context.SaveChanges();
            return RedirectToAction("detail", new { id = review.RoomId });
        }
        private RoomDetailViewModel _getRoomDetail(int id)
        {
            var room = _context.Rooms
              .Include(x => x.RoomReviews)
              .ThenInclude(x => x.AppUser)
              .FirstOrDefault(x => x.Id == id);
            RoomDetailViewModel vm = new RoomDetailViewModel()
            {
                Room = room,
                Review = new RoomReview { RoomId = id }
            };
            return vm;
        }

        public IActionResult GetSearch(string searchValue)
        {
            var datas = _context.Rooms.Where(x => x.Name.Contains(searchValue)).ToList();
            return Json(datas);
        }
    }
}
