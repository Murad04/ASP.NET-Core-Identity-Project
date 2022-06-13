using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_Identity.Data.Account;

namespace Project_Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User> signinmanager;


        public LogoutModel(SignInManager<User> signInManager)
        {
            this.signinmanager = signInManager;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToPage("/Account/Login");
        }
    }
}
