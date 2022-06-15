using System.ComponentModel.DataAnnotations;

namespace Project_Identity.ViewModels
{
    public class UserProfileViewModel
    {
        public string Email { get; set; } = null!;

        [Required]
        public string Department { get; set; }= null!;

        [Required]
        public string Position { get; set; } = null!;
    }
}
