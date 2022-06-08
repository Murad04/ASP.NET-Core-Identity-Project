using System.ComponentModel.DataAnnotations;

namespace Project_Identity.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(dataType: DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
