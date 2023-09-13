using CodeFinalProject.Attributes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFinalProject.Models
{
    public class Room
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int RoomNumber { get; set; }
        public int MaxPersonCount { get; set; }
        public bool IsReserved { get; set; }
        public byte Rate { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public double Price { get; set; }
        public int ElderCount { get; set; }
        public int ChildCount { get; set; }
        [Required]
        [MaxLength(50)]
        public string BtnText { get; set; }
        [Required]
        [MaxLength(50)]
        public string BtnUrl { get; set; }
        [MaxLength(100)]
        public string Image { get; set; }
        [NotMapped]
        [FileMaxLength(2097152)]
        [AllowContentType("image/jpeg", "image/png")]
        public IFormFile ImageFile { get; set; }
        public List<Reservation> Reservation { get; set; }
        public List<RoomReview> RoomReviews { get; set; } = new List<RoomReview>();
    }
}
