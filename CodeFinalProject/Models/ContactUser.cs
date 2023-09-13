using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CodeFinalProject.Models
{
    public class ContactUser
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        [Required]
        [MaxLength(25)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(15)]
        public string Phone { get; set; }
        [Required]
        [MaxLength(80)]
        public string Email { get; set; }
        [MaxLength(200)]
        public string Text { get; set; }
        public AppUser AppUser { get; set; }
        [NotMapped]
        public string Response { get; set; }
    }
}
