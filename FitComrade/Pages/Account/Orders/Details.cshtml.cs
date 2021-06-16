using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Domain.Entities;
using FitComrade.Models;
using FitComrade.Core.Services;

namespace FitComrade.Pages.Account.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly IDataService _service;

        public DetailsModel(IDataService service)
        {
            _service = service;
        }

        public Order Order;
        public List<OrderDetail> OrderDetail;
        public Customer Customer;
        public List<Payment> Payments;
        public List<CustomerAdress> CustomerAdress;
        public List<Product> Products;
        private SessionUser sessionUser = new SessionUser();


        public IActionResult OnGet(int? id) // Haalt OrderDetails op met specifiek id
        {
            if (id == null) // Er is geen order met het specifieke id
            {
                return NotFound();
            }

            sessionUser = sessionUser.GetSession(HttpContext.Session);

            OrderDetail = _service.GetOrderDetails().Where(m => m.OrderID.Equals(id)).ToList();

            Products = _service.GetProducts();

            Order = _service.GetOrders().FirstOrDefault(m => m.OrderID == id);

            if (Order == null || sessionUser.ProfileID != Order.CustomerID && sessionUser.ProfileID != 1) // Er zijn geen orders of gebruiker heeft geen toegang
            {
                return NotFound();
            }

            Customer = _service.GetCustomers().FirstOrDefault(m => m.CustomerID == Order.CustomerID);

            Payments = _service.GetPayments().ToList();

            CustomerAdress = _service.GetCustomerAdresses().ToList();


            return Page();
        }
    }
}
