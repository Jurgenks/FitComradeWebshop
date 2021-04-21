using FitComrade.Data;
using FitComrade.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitComrade.Pages.Login
{
    public class SignUpModel : PageModel
    {
        private readonly Data.FitComradeContext _context;

        public SignUpModel(Data.FitComradeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Profile Profile { get; set; }
        [BindProperty]
        public Profile Match { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if(Profile.Password == Match.Password)  //Check of registratie gegevens kloppen
            {                
                DataController dataController = new DataController(_context);
                bool succes = dataController.Create(Profile);
                if(succes == true)  //Account aangemaakt
                {
                    return RedirectToPage("Login/SignIn");
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
