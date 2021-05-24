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
    public class RedeemModel : PageModel
    {
        private readonly FitComradeContext _context;

        public RedeemModel(FitComradeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CreditCode CreditCode { get; set; }

        public SessionUser user = new SessionUser();

        public void OnGet()
        {
            user = user.GetSession(HttpContext.Session, user);
            if (user.ProfileID == 0)
            {
                Response.Redirect("/");
            }           

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(CreditCode.CreditCodeString != null && CreditCode.CreditCodeString.Length <= 10)
            {
                CreditController creditController = new CreditController(_context);
                bool succes = creditController.RedeemCode(HttpContext.Session, CreditCode);
                if(succes == true)
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Account/Index");
                }
                else
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
