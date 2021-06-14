using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitComrade.Data;
using FitComrade.Domain.Entities;
using FitComrade.Helpers;
using FitComrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitComrade.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly FitComradeContext _context;

        public IndexModel(FitComradeContext context)
        {
            _context = context;
        }

        public SessionUser SessionUser = new SessionUser();

        public Customer Customer { get; set; }

        public Credit Credits { get; set; }

        public void OnGet()
        {
            SessionUser = SessionUser.GetSession(HttpContext.Session);
            if(SessionUser.ProfileID != 0)
            {
                Customer = _context.Customers.FirstOrDefault(item => item.CustomerID.Equals(SessionUser.ProfileID));
                Credits = _context.Credits.FirstOrDefault(item => item.CustomerID.Equals(SessionUser.ProfileID));
            }

        }
        
    }
}
