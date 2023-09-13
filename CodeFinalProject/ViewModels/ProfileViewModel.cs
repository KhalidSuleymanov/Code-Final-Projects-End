using CodeFinalProject.Models;

namespace CodeFinalProject.ViewModels
{
    public class ProfileViewModel
    {
        public MemberUpdateViewModel Member { get; set; }
        public List<Reservation> Reservations { get; set; }
        //public List<Order> Orders { get; set; }
    }
}
