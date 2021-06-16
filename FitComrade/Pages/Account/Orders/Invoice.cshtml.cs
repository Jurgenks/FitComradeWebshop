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
using FitComrade.Core.Services;

namespace FitComrade.Pages.Account.Orders
{
    public class IndexModel : PageModel
    {
        private readonly FitComradeContext _context;

        public IndexModel(FitComradeContext context)
        {
            _context = context;
        }

        public List<Order> Order { get; set; }

        public SessionUser SessionUser = new SessionUser();

        public async Task OnGetAsync() //Bij bezoek van pagina worden de orders van de customer opgehaald
        {
            SessionUser = SessionUser.GetSession(HttpContext.Session);
            if (SessionUser.ProfileID == 0)
            {
                Response.Redirect("/");
            }
            var data = _context.Orders.Where(order => order.CustomerID.Equals(SessionUser.CustomerID));
            if (data.Count() > 0)
            {
                Order = await data.ToListAsync();
            }

        }
        public async Task OnGetConfirmAsync(int id) //Na het bevestigen van de Order is het klaar voor verzameling
        {
            SessionUser = SessionUser.GetSession(HttpContext.Session);

            if (SessionUser.ProfileID == 1)
            {
                var order = _context.Orders.Where(s => s.OrderID.Equals(id)).FirstOrDefault();

                OrderService dataController = new OrderService(_context);

                dataController.UpdateStatus(order);

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
        public async Task OnGetDismissed(int id) //Bij het bevestigen van de order naar "Dismissed" wordt de voorraad teruggehaald
        {
            SessionUser = SessionUser.GetSession(HttpContext.Session);

            if (SessionUser.ProfileID > 0)
            {
                var order = _context.Orders.Where(s => s.OrderID.Equals(id)).FirstOrDefault();

                OrderService dataController = new OrderService(_context);

                dataController.RetourOrder(order);

                var data = _context.Orders.Where(order => order.CustomerID.Equals(SessionUser.CustomerID));
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
            SessionUser = SessionUser.GetSession(HttpContext.Session);
            if (SessionUser.ProfileID == 1)
            {
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
    }
}
