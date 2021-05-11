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

namespace FitComrade.Pages.Account.Orders
{
    public class IndexModel : PageModel
    {
        private readonly FitComradeContext _context;

        public IndexModel(FitComradeContext context)
        {
            _context = context;
        }

        public List<Order> Order { get;set; }

        public SessionUser user = new SessionUser();

        public async Task OnGetAsync() //Bij bezoek van pagina worden de orders van de customer opgehaald
        {
            user = user.GetSession(HttpContext.Session, user);
            if(user.ProfileID == 0)
            {
                Response.Redirect("/");
            }
            var data = _context.Orders.Where(order=>order.CustomerID.Equals(user.CustomerID));
            if (data.Count() > 0)
            {
                Order = await data.ToListAsync();
            }            
            
        }
        public async Task OnGetConfirmAsync(int id) //Pas bij het bevestigen van de order naar "Confirmed" wordt de voorraad afgeschreven
        {
            user = user.GetSession(HttpContext.Session, user);

            if(user.ProfileID == 1)
            {
                var order = _context.Orders.Where(s => s.OrderID.Equals(id)).FirstOrDefault();

                DataController dataController = new DataController(_context);

                dataController.UpdateStock(order);

                var data = _context.Orders;
                if (data.Count() > 0)
                {
                    Order = await data.ToListAsync();
                }
            }
            else
            {
                await OnGetAsync();
            }
            
        }
        public async Task OnGetAllOrdersAsync() // Haalt alle orders op
        {
            user = user.GetSession(HttpContext.Session, user);
            if(user.ProfileID == 1)
            {
                var data = _context.Orders;
                if(data.Count()>0)
                {
                    Order = await data.ToListAsync();
                }
            }
            else
            {
                await OnGetAsync();
            }
        }
    }
}
