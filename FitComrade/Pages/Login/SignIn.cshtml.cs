using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitComrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Domain.Entities;
using FitComrade.Data;
using FitComrade.Core.Controller;

namespace FitComrade.Pages.Login
{
    public class SignInModel : PageModel
    {
        private readonly Data.FitComradeContext _context;

        public SignInModel(Data.FitComradeContext context)
        {
            _context = context;
        }

        private SessionUser sessionUser = new SessionUser();

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

            if (Succes == true && sessionUser.GetAttemptLogin(HttpContext.Session) < 5) //Login succes en wordt doorgestuurd naar Account
            {
                sessionUser = sessionUser.GetSession(HttpContext.Session);
                return RedirectToPage("/Account/Index");
            }
            else //Login fout wordt opgeteld in de sessie
            {
                sessionUser.AttemptLogin(HttpContext.Session);
                return Page();
            }

        }
    }
}
