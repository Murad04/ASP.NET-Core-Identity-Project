using Microsoft.AspNetCore.Identity;

namespace Project_Identity.Data.Account
{
    public class User:IdentityUser
    {
        public string Department { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string Department_Code { get; set; }= null!;
    }
}
