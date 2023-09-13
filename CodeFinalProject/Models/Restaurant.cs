using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFinalProject.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get;set; }
        [Required]
        [Column(TypeName = "text")]
        public string Description1 { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Description2 { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Description3 { get; set; }
    }
}
