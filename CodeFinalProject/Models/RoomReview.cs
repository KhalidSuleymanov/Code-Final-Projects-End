using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CodeFinalProject.Models
{
    public class RoomReview
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string AppUserId { get; set; }
        [Range(1, 5)]
        public byte Rate { get; set; }
        [Column(TypeName = "text")]
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public AppUser AppUser { get; set; }
        public Room Room { get; set; }
    }
}
