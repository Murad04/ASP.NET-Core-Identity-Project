using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_Identity.Data.Account;
using Project_Identity.ViewModels;
using System.Security.Claims;

namespace Project_Identity.Pages.Account
{
    [Authorize]
    public class UserProfileModel : PageModel
    {
        private readonly UserManager<User> userManager;

        [BindProperty]
        public UserProfileViewModel UserProfile { get; set; }

        [BindProperty]
        public string? Message { get; set; }

        public UserProfileModel(UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.UserProfile = new UserProfileViewModel();
            this.Message = string.Empty;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            this.Message = string.Empty;

            var (user, departmentClaim, positionClaim) = await GetUserInfoAsyc();

            this.UserProfile.Email = User.Identity.Name;
            this.UserProfile.Department = departmentClaim?.Value;
            this.UserProfile.Position = positionClaim?.Value;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            try
            {
                var (user, departmentClaim, positionClaim) = await GetUserInfoAsyc();
                await userManager.ReplaceClaimAsync(user, departmentClaim, new Claim(departmentClaim.Type, UserProfile.Department));
                await userManager.ReplaceClaimAsync(user, positionClaim, new Claim(positionClaim.Type, UserProfile.Position));
            }
            catch
            {
                ModelState.AddModelError("UserProfile", "Error");
            }

            this.Message = "User profile saved";

            return Page();
        }
        private async Task<(Data.Account.User, Claim, Claim)> GetUserInfoAsyc()
        {
            User? Users = await userManager.FindByNameAsync(userName: User.Identity.Name);
            var Claims = await userManager.GetClaimsAsync(Users);
            var departmentclaim = Claims.FirstOrDefault(c => c.Type == "Department");
            var positionclaim = Claims.FirstOrDefault(c => c.Type == "Position");

            return (Users, departmentclaim, positionclaim);
        }
    }
}
