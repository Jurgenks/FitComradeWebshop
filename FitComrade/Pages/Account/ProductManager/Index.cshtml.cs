using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitComrade.Data;
using FitComrade.Models;
using FitComrade.Domain.Entities;

namespace FitComrade.Pages.Account.ProductManager
{
    public class IndexModel : PageModel
    {
        private readonly FitComradeContext _context;

        public IndexModel(FitComradeContext context)
        {
            _context = context;
        }

        private SessionUser sessionUser = new SessionUser();

        public IList<Product> Products { get; private set; }

        public List<OrderDetail> OrderDetails { get; private set; }

        public async Task OnGetAsync()
        {
            sessionUser = sessionUser.GetSession(HttpContext.Session);
            if (sessionUser.ProfileID == 1)
            {
                Products = await _context.Products.ToListAsync();

                OrderDetails = _context.OrderDetails.ToList();
            }
            else
            {
                Response.Redirect("/");
            }

        }


    }
}
