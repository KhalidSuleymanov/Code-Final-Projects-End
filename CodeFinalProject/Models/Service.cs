using System.ComponentModel.DataAnnotations;

namespace CodeFinalProject.Models
{
    public class Service
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string IconImage { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
