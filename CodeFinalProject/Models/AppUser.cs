using Microsoft.AspNetCore.Identity;

namespace CodeFinalProject.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool IsAdmin { get; set; }
        public string ConnectionId { get; set; }
        public bool IsOnline { get; set; }
        public DateTime LastOnlineAt { get; set; }
        public List<UserContact> Messages { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
