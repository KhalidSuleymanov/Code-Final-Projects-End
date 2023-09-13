using CodeFinalProject.DAL;
using CodeFinalProject.Email;
using CodeFinalProject.Enums;
using CodeFinalProject.Models;
using CodeFinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CodeFinalProject.Controllers
{
    [Authorize()]
    public class BookOnlineController : Controller
    {
        private readonly HimaraDbContext _context;
        private readonly IMailService _mailService;
        public BookOnlineController(
            HimaraDbContext context,
            IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }
        public IActionResult Index(int roomid)
        {
            var rooms = _context.Rooms.ToList();
            var bookOnlineVM = new BookOnlineVM()
            {
                Rooms = rooms,
                RoomId = roomid,
                Room = _context.Rooms.FirstOrDefault(r=>r.Id == roomid)

            };
            return View(bookOnlineVM);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BookOnlineVM bookOnlineVm)
        {
            bookOnlineVm.Rooms = _context.Rooms.ToList();

            if (ModelState.IsValid)
            {
                #region RoomIdCheck

                var room = await _context.Rooms
                    .Where(p => p.Id == bookOnlineVm.RoomId)
                    .FirstAsync();
                if (room is null)
                {
                    ModelState.AddModelError("RoomId", $"Room Id {bookOnlineVm.RoomId} is not available");
                    return View(bookOnlineVm);
                }

                #endregion

                #region Checkifreserved
                bool flag = IsReservationDateRangeValid(bookOnlineVm.ReservStartDate, bookOnlineVm.ReservEndDate,
                    bookOnlineVm.RoomId);
                if (!flag)
                {
                    bookOnlineVm.IsReserved = true;
                    bookOnlineVm.ReservedRooms = await _context.Reservation
                        .Where(p => p.RoomId == bookOnlineVm.RoomId
                                    && p.ReservEndDate > DateTime.Now
                                    && p.IsDeleted == false)
                        .ToListAsync();
                    ModelState.AddModelError("ReservStartDate",
                        "this table is reserved for the date range you specified");
                    return View(bookOnlineVm);
                }

                #endregion

                var reserv = new Reservation
                {
                    FullName = bookOnlineVm.FullName,
                    Email = bookOnlineVm.Email,
                    RoomId = bookOnlineVm.RoomId,
                    Additionals = bookOnlineVm.Additionals,
                    IsActive = true,
                    PhoneNumber = bookOnlineVm.PhoneNumber,
                    ReservDate = bookOnlineVm.ReservStartDate,
                    ReservEndDate = bookOnlineVm.ReservEndDate,
                    ReservationStatus = ReservationStatusEnum.Pending,
                    AppUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
                };
                _context.Reservation.Add(reserv);
                _context.SaveChanges();
                bookOnlineVm.IsSuccess = true;

                // var mailReq = new MailRequest();
                // mailReq.ToEmail = bookOnlineVm.Email;
                // mailReq.Subject = "Himara";
                // mailReq.Body = $"Room:{room.RoomNumber}\n" +
                //                $"StartDate:{bookOnlineVm.ReservStartDate}\n" +
                //                $"EndDate:{bookOnlineVm.ReservEndDate}\n";
                // await _mailService.SendEmailAsync(mailReq);
                return View(bookOnlineVm);
            }
            return View(bookOnlineVm);
        }

        public bool IsReservationDateRangeValid(DateTime reservDate, DateTime reservEndDate, int roomId)
        {
            var overlappingReservations = _context.Reservation
                .Where(r => r.RoomId == roomId && r.IsActive)
                .ToList();
            foreach (var reservation in overlappingReservations)
            {
                if (!(reservEndDate < reservation.ReservDate || reservDate > reservation.ReservEndDate))
                {
                    return false;
                }
            }
            return true;
        }
    }
}