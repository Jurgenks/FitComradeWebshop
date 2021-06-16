using FitComrade.Core.Services;
using FitComrade.Data;
using FitComrade.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitComrade.Pages.Login
{
    public class SignUpModel : PageModel
    {
        private readonly IAccountService _accountService;

        public SignUpModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public Customer Profile { get; set; }
        [BindProperty]
        public Customer Match { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (Profile.Password == Match.Password)  //Check of registratie gegevens kloppen
            {                
                bool succes = _accountService.CreateProfile(Profile);

                if (succes == true)  //Account aangemaakt
                {
                    return RedirectToPage("SignIn");
                }
                else //Account bestaat al
                {
                    return Page();
                }

            }
            else
            {
                return Page();
            }
        }
    }
}
