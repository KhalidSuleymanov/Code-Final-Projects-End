using CodeFinalProject.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CodeFinalProject.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Additionals { get; set; }
        public DateTime ReservDate { get; set; }
        public DateTime ReservEndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ReservationStatusEnum ReservationStatus { get; set; }
        // public int TimeIntervalId { get; set; }
        // public TimeInterval TimeInterval { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
    }
}
