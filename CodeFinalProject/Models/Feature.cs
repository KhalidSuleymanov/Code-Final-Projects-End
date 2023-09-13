using System.ComponentModel.DataAnnotations;

namespace CodeFinalProject.Models
{
    public class Feature
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string IconImage { get; set; }
        [Required]
        public int FeatureCount { get; set; }
    }
}
