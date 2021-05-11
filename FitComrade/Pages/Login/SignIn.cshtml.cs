using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitComrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Domain.Entities;
using FitComrade.Data;
using FitComrade.Core;

namespace FitComrade.Pages.Login
{
    public class SignInModel : PageModel
    {
        private readonly Data.FitComradeContext _context;

        public SignInModel(Data.FitComradeContext context)
        {
            _context = context;
        }

        public SessionUser user = new SessionUser();
        [BindProperty]
        public Customer Profile { get; set; }
        

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {            
            DataController dataController = new DataController(_context);

            //Check of inloggegevens kloppen
            bool Succes = dataController.Login(HttpContext.Session, Profile);

            if(Succes == true && user.GetAttemptLogin(HttpContext.Session) < 5) //Login succes en wordt doorgestuurd naar Account
            {
                user = user.GetSession(HttpContext.Session, user);
                return RedirectToPage("/Account/Index");
            }
            else //Login fout wordt opgeteld in de sessie
            {                
                user.AttemptLogin(HttpContext.Session); 
                return Page();
            }
            
        }
    }
}
