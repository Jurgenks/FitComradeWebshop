using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitComrade.Data;
using FitComrade.Models;

namespace FitComrade.Pages.Account.ProductManager
{
    public class IndexModel : PageModel
    {
        private readonly FitComradeContext _context;

        public IndexModel(FitComradeContext context)
        {
            _context = context;
        }
        public SessionUser user = new SessionUser();
        public IList<Product> Products { get;set; }
        private int admin = 1;

        public async Task OnGetAsync()
        {
            user = user.GetSession(HttpContext.Session, user);
            if (user.ProfileID == admin)
            {
                Products = await _context.Products.ToListAsync();
            }
            else
            {
                Response.Redirect("/");
            }
            
        }
        
        
    }
}
