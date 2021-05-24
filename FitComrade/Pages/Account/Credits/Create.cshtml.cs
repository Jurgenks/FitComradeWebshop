using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitComrade.Core.Controller;
using FitComrade.Data;
using FitComrade.Domain.Entities;
using FitComrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitComrade.Pages.Account.Credits
{
    public class CreateModel : PageModel
    {
        private readonly FitComradeContext _context;

        public CreateModel(FitComradeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CreditCode CreditCode { get; set; }

        public SessionUser user = new SessionUser();

        public void OnGet()
        {
            user = user.GetSession(HttpContext.Session, user);
            if (user.ProfileID != 1)
            {
                Response.Redirect("/");
            }

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(CreditCode.CreditCodeString.Length > 10)
            {
                return Page();
            }
            CreditController creditController = new CreditController(_context);
            bool created = creditController.CreateCode(HttpContext.Session, CreditCode);

            if (created == true)
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
