using CodeFinalProject.Attributes;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFinalProject.Models
{
    public class AboutService
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Image { get; set; }
        [NotMapped]
        [FileMaxLength(2097152)]
        [AllowContentType("image/jpeg", "image/png")]
        public IFormFile ImageFile { get; set; }
    }
}
