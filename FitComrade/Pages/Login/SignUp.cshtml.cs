using FitComrade.Core.Services;
using FitComrade.Data;
using FitComrade.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitComrade.Pages.Login
{
    public class SignUpModel : PageModel
    {
        private readonly FitComradeContext _context;

        public SignUpModel(FitComradeContext context)
        {
            _context = context;
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
                AccountService accountService = new AccountService(_context);
                bool succes = accountService.CreateProfile(Profile);

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
