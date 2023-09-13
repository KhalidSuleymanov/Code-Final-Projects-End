using CodeFinalProject.Areas.Manage.ViewModels;
using CodeFinalProject.DAL;
using CodeFinalProject.Email;
using CodeFinalProject.Enums;
using CodeFinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFinalProject.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    [Area("manage")]
    public class ReservationController : Controller
    {
        private HimaraDbContext _context;
        private readonly IMailService _mailService;
        public ReservationController(HimaraDbContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Search = search;
            var query = _context.Reservation.Where(p => p.IsDeleted == false).AsQueryable();
            if (search != null)
            {
                query = query.Where(x => x.FullName.Contains(search));
            }
            return View(PaginatedList<Reservation>.Create(query, page, 5));
        }
        public IActionResult Delete(int id)
        {
            var existRoom = _context.Reservation.Find(id);
            if (existRoom == null)
            {
                return View("Error");
            }

            existRoom.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Reject(int id)
        {
            var existRoom = await _context.Reservation
                .Where(p => p.Id == id)
                .Include(p => p.Room)
                .FirstOrDefaultAsync();
            if (existRoom == null)
            {
                return View("Error");
            }
            existRoom.ReservationStatus = ReservationStatusEnum.Rejected;
            await _context.SaveChangesAsync();
            await SendMailAsync(existRoom.Email,
                existRoom.Room.RoomNumber,
                existRoom.ReservDate,
                existRoom.ReservEndDate,
                ReservationStatusEnum.Rejected);
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Approve(int id)
        {
            var existRoom = await _context.Reservation
                .Where(p => p.Id == id)
                .Include(p => p.Room)
                .FirstOrDefaultAsync();
            if (existRoom == null)
            {
                return View("Error");
            }
            existRoom.ReservationStatus = ReservationStatusEnum.Aproved;
            await _context.SaveChangesAsync();
            await SendMailAsync(existRoom.Email,
                existRoom.Room.RoomNumber,
                existRoom.ReservDate,
                existRoom.ReservEndDate,
                ReservationStatusEnum.Aproved);
            return RedirectToAction("index");
        }
        private async Task SendMailAsync(string email, int roomNumber, DateTime reservStartDate, DateTime reservEndDate, ReservationStatusEnum reservationStatus)
        {
            var mailReq = new MailRequest();
            mailReq.ToEmail = email;
            mailReq.Subject = "Himara";



            if (reservationStatus.ToString() == "Aproved")
            {
                mailReq.Body = $"Salam. Sizin rezervasiyanız qəbul olundu.\n" +

                $"Room:{roomNumber}\n" +

                $"StartDate:{reservStartDate}\n" +

                $"EndDate:{reservEndDate}\n" +

                $"Status:{reservationStatus.ToString()}";
            }
            else
            {
                mailReq.Body = $"Salam. Təəsüflər olsun ki, sizin seçdiyiniz otaqda təmir işləri gedir.\n" +
                    $" Bu səbəbdən sizdən başqa bir otaq seçməyinizi xahiş edirik.\n" +
                    $"Hörmətlə: Himara Hotel";
            }

            await _mailService.SendEmailAsync(mailReq);
        }
    }
}