using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_Identity.Data.Account;
using Project_Identity.ViewModels;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;

namespace Project_Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<User> userManager;
        public RegisterModel(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [BindProperty]
        public RegisterViewModel registerViewModel { get; set; } = null!;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            //Validating email address                  

            //Create the user
            var user = new User
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email,
                Department = registerViewModel.Department,
                Position = registerViewModel.Position,
                Department_Code = registerViewModel.Department_Code
            };

            //var claimDepartmentCode = new Claim("Department_Code", registerViewModel.Department_Code);

            var departmentclaim = new Claim("Department", registerViewModel.Department);

            var positionclaim = new Claim("Position", registerViewModel.Position);

            var result = await this.userManager.CreateAsync(user, registerViewModel.Password);
            if (result.Succeeded)
            {
                await this.userManager.AddClaimAsync(user, departmentclaim);
                await this.userManager.AddClaimAsync(user, positionclaim);

                var confirmationToken = await this.userManager.GenerateEmailConfirmationTokenAsync(user);

                return Redirect(Url.PageLink(pageName: "/Account/Email/ConfirmEmail", values: new { userID = user.Id, token = confirmationToken }));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }
                return Page();
            }
        }
    }
}
