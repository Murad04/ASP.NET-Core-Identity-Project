using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using UdemyASP.NETCOREIdenity.Authorization;

namespace UdemyASP.NETCOREIdenity.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credentials Credentials { get; set; } = null!;
        public void OnGet()
        {
            this.Credentials = new Credentials { Name = "Murad" };
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            //verify credential
            if (Credentials.Name == "Murad" && Credentials.Password == "password")
            {
                //creating the security context
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Murad"),
                    new Claim(ClaimTypes.Email, "deneme@dede.com"),
                    new Claim("Department","HR"),
                    new Claim("Manager","true"),
                    new Claim("Admin","true"),
                    new Claim("EmploymentDate","2021-02-24")
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = Credentials.RememberMe
                };

                await HttpContext.SignInAsync("MyCookieAuth", principal, authProperties);

                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
    
}
