using CodeFinalProject.Attributes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFinalProject.Models
{
    public class Meal
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public int Price { get; set; }
        public string Image { get; set; }
        [NotMapped]
        [FileMaxLength(2097152)]
        [AllowContentType("image/jpeg", "image/png")]
        public IFormFile ImageFile { get; set; }
        public List<MealIngredient> MealIngredients { get; set; }
    }
}
