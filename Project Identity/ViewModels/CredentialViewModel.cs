using System.ComponentModel.DataAnnotations;

namespace Project_Identity.ViewModels
{
    public class CredentialViewModel
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name ="Remember me")]
        public bool RememberMe { get; set; }
    }
}
