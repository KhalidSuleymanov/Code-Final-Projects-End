using CodeFinalProject.Models;

namespace CodeFinalProject.ViewModels;

public class BookOnlineVM
{
    public List<Room> Rooms { get; set; }
    public Room Room { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Additionals { get; set; }
    public DateTime ReservStartDate { get; set; } = DateTime.Now.AddDays(1).Date;
    public DateTime ReservEndDate { get; set; } = DateTime.Now.AddDays(2).Date;
    public bool IsSuccess { get; set; }
    public bool IsReserved { get; set; }
    public List<Reservation> ReservedRooms { get; set; }
    public int RoomId { get; set; }
}