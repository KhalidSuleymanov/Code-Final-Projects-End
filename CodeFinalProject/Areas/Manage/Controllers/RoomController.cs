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
    public class RoomController : Controller
    {
        private readonly HimaraDbContext _context;
        private readonly IWebHostEnvironment _env;
        public RoomController(HimaraDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Search = search;
            var query = _context.Rooms.AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.Name.Contains(search));
            }
            return View(PaginatedList<Room>.Create(query, page, 5));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Room room)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (room.ImageFile == null)
            {
                ModelState.AddModelError("Image", "Image file is required");
                return View();
            }
            room.Image = FileManager.Save(room.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/RoomImage");
            room.MaxPersonCount = room.ChildCount + room.ElderCount;
            _context.Rooms.Add(room);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            var existRoom = _context.Rooms.Find(id);
            if (existRoom == null)
            {
                return View("Error");
            }
            return View(existRoom);
        }

        [HttpPost]
        public IActionResult Edit(Room room)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var existRoom = _context.Rooms.Find(room.Id);
            if (existRoom == null)
            {
                return View("Error");
            }
            string removableImage = null;
            if (room.ImageFile != null)
            {
                removableImage = existRoom.Image;
                existRoom.Image = FileManager.Save(room.ImageFile, _env.WebRootPath, "Manage/Assets/Uploads/RoomImage");
            }
            existRoom.Name = room.Name;
            existRoom.Description = room.Description;
            existRoom.Price = room.Price;
            existRoom.BtnText = room.BtnText;
            existRoom.BtnUrl = room.BtnUrl;
            existRoom.ChildCount = room.ChildCount;
            existRoom.ElderCount = room.ElderCount;
            existRoom.MaxPersonCount = room.ChildCount+room.ElderCount;
            existRoom.RoomNumber = room.RoomNumber;
            _context.SaveChanges();
            if (removableImage != null)
            {
                FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/RoomImage", removableImage);
            }
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            var existRoom = _context.Rooms.Find(id);
            if (existRoom == null)
            {
                return View("Error");
            }
            var removableImage = existRoom.Image;
            _context.Rooms.Remove(existRoom);
            _context.SaveChanges();
            FileManager.Delete(_env.WebRootPath, "Manage/Assets/Uploads/RoomImage", removableImage);
            return RedirectToAction("index");
        }
    }
}
