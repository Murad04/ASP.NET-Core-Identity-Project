using System.ComponentModel.DataAnnotations;

namespace UdemyASP.NETCOREIdenity.Authorization
{
    public class Credentials
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
