using System.ComponentModel.DataAnnotations;

namespace CodeFinalProject.Models
{
    public class RecentPost
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
    }
}
