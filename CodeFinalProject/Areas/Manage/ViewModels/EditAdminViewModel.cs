using System.ComponentModel.DataAnnotations;

namespace CodeFinalProject.Areas.Manage.ViewModels
{
    public class EditAdminViewModel
    {
        public string Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(25)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(25)]
        public string Role { get; set; }
    }
}
