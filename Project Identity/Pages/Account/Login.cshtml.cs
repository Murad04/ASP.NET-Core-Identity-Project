using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_Identity.Data.Account;
using Project_Identity.ViewModels;

namespace Project_Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> signInManager;

        public LoginModel(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        [BindProperty]
        public CredentialViewModel Credential { get; set; } = null!;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var result = await signInManager.PasswordSignInAsync(this.Credential.Email, this.Credential.Password, this.Credential.RememberMe, false);

            if (result.Succeeded)
                return RedirectToPage("/Index");
            else
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("Login", "You are locked out");
                }
                else
                {
                    ModelState.AddModelError("Login", "Failed to login");
                }
            }
            return Page();
        }
    }
}
