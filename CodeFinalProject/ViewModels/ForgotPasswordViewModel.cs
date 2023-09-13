using System.ComponentModel.DataAnnotations;

namespace CodeFinalProject.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [MaxLength(25)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
