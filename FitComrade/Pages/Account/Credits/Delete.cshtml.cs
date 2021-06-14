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
using Microsoft.EntityFrameworkCore;

namespace FitComrade.Pages.Account.Credits
{
    public class DeleteModel : PageModel
    {
        private readonly FitComradeContext _context;

        public DeleteModel(FitComradeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CreditCode CreditCode { get; set; }

        private SessionUser sessionUser = new SessionUser();


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            sessionUser = sessionUser.GetSession(HttpContext.Session);
            if (sessionUser.ProfileID != 1)
            {
                Response.Redirect("/");
            }

            if (id == null)
            {
                return NotFound();
            }

            CreditCode = await _context.CreditCodes.FirstOrDefaultAsync(m => m.CreditCodeID == id);

            if (CreditCode == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CreditCode = await _context.CreditCodes.FindAsync(id);

            if (CreditCode != null && CreditCode.CreditIsValid == true)
            {
                CreditController creditController = new CreditController(_context);
                creditController.DeleteCode(HttpContext.Session, CreditCode);
            }

            return RedirectToPage("./Index");
        }
    }
}
