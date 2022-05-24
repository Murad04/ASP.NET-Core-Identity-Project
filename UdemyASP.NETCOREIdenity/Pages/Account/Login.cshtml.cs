using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace UdemyASP.NETCOREIdenity.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credentials { get; set; } = null!;
        public void OnGet()
        {
            this.Credentials = new Credential { Name = "murad" };
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            //verify credential
            if (Credentials.Name == "murad" && Credentials.Password == "password")
            {
                //creating the security context
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "murad"),
                    new Claim(ClaimTypes.Email, "deneme@dede.com"),
                    new Claim("Department","HR"),
                    new Claim("AdminOnly","true")
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", principal);

                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
    public class Credential
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
