using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Models;
using FitComrade.Domain.Entities;
using FitComrade.Core.Services;

namespace FitComrade.Pages.Account.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IDataService _service;

        private readonly IOrderService _orderService;

        public IndexModel(IDataService service, IOrderService orderService)
        {
            _service = service;
            _orderService = orderService;
        }

        public List<Order> Order { get; private set; }

        public SessionUser SessionUser = new SessionUser();

        public void OnGet() //Bij bezoek van pagina worden de orders van de customer opgehaald
        {
            SessionUser = SessionUser.GetSession(HttpContext.Session);

            if (SessionUser.ProfileID == 0)
            {
                Response.Redirect("/");
            }

            var data = _service.GetOrders().Where(order => order.CustomerID.Equals(SessionUser.CustomerID));

            if (data.Count() > 0)
            {
                Order = data.ToList();
            }

        }
        public void OnGetConfirm(int id) //Na het bevestigen van de Order is het klaar voor verzameling
        {
            SessionUser = SessionUser.GetSession(HttpContext.Session);

            if (SessionUser.ProfileID == 1)
            {
                var order = _service.GetOrders().Where(s => s.OrderID.Equals(id)).FirstOrDefault();

                _orderService.UpdateStatus(order);

                var data = _service.GetOrders();

                if (data.Count() > 0)
                {
                    Order = data.ToList();
                }
            }
            else
            {
                OnGet();
            }

        }
        public void OnGetDismissed(int id) //Bij het bevestigen van de order naar "Dismissed" wordt de voorraad teruggehaald
        {
            SessionUser = SessionUser.GetSession(HttpContext.Session);

            if (SessionUser.ProfileID > 0)
            {
                var order = _service.GetOrders().Where(s => s.OrderID.Equals(id)).FirstOrDefault();

                _orderService.RetourOrder(order);

                var data = _service.GetOrders().Where(order => order.CustomerID.Equals(SessionUser.CustomerID));

                if (data.Count() > 0)
                {
                    Order = data.ToList();
                }
            }
            else
            {
                OnGet();
            }

        }
        public void OnGetAllOrders() // Haalt alle orders op
        {
            SessionUser = SessionUser.GetSession(HttpContext.Session);

            if (SessionUser.ProfileID == 1)
            {
                var data = _service.GetOrders();

                if (data.Count() > 0)
                {
                    Order = data.ToList();
                }
            }
            else
            {
                OnGet();
            }
        }
    }
}
