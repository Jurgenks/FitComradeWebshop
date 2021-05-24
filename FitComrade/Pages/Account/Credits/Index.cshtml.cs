using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitComrade.Data;
using FitComrade.Domain.Entities;
using FitComrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FitComrade.Pages.Account.Credits
{
    public class IndexModel : PageModel
    {
        private readonly FitComradeContext _context;

        public IndexModel(FitComradeContext context)
        {
            _context = context;
        }

        public List<CreditCode> CreditCodes { get; set; }

        public SessionUser user = new SessionUser();

        public async Task OnGet()
        {
            user = user.GetSession(HttpContext.Session, user);
            if (user.ProfileID != 1)
            {
                Response.Redirect("/");
            }

            CreditCodes = await _context.CreditCodes.ToListAsync();
        }        
    }
}
