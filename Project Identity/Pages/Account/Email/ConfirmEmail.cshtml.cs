using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_Identity.Data.Account;

namespace Project_Identity.Pages.Account.Email
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<User> userManager;

        [BindProperty]
        public string Message { get; set; } = null!;

        public ConfirmEmailModel(UserManager<User> userManager)
        {
            this.userManager=userManager;
        }
        public async Task<IActionResult> OnGetAsync(string userID,string token)
        {
            var user = await this.userManager.FindByIdAsync(userID);
            if(user != null)
            {
                var result = await this.userManager.ConfirmEmailAsync(user, token);
                if(result.Succeeded)
                {
                    this.Message = "Email address is successfully confirmed";
                    return Page();
                }
            }
            this.Message = "Failed to validate email";
            return Page();
        }
    }
}
