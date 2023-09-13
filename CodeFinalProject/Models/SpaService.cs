using System.ComponentModel.DataAnnotations;

namespace CodeFinalProject.Models
{
    public class SpaService
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string IconImage { get; set; }
    }
}
