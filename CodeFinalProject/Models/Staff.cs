using CodeFinalProject.Attributes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFinalProject.Models
{
    public class Staff
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Description { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(20)]
        public string Position { get; set; }
        [MaxLength(100)]
        public string Image { get; set; }
        [NotMapped]
        [FileMaxLength(2097152)]
        [AllowContentType("image/jpeg", "image/png")]
        public IFormFile ImageFile { get; set; }
    }
}
