using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Models;
using FitComrade.Helpers;
using FitComrade.Domain.Entities;
using FitComrade.Core.Services;
using FitComrade.Data;
using System.Linq;

namespace FitComrade.Pages.Shop
{
    public class CheckoutModel : PageModel
    {
        private readonly IDataService _service;
        private readonly IOrderService orderService;

        public CheckoutModel(IDataService service, IOrderService order)
        {
            _service = service;
            orderService = order;
        }

        public SessionUser SessionUser = new SessionUser();

        public Cart Cart = new Cart();
        [BindProperty]
        public Customer Customer { get; set; }
        [BindProperty]
        public CustomerAdress CustomerAdress { get; set; }
        [BindProperty]
        public Payment Payment { get; set; }

        public List<Payment> Payments { get; private set; }


        public void OnGet()
        {
            Cart.Products = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "cart");

            SessionUser = SessionUser.GetSession(HttpContext.Session);

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart.Products);

            Payments = _service.GetPayments();

            if (Cart.Products == null || Cart.Products.Count == 0)
            {
                Response.Redirect("/");
            }
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Cart.Products = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "cart");

            orderService.RegisterCustomer(HttpContext.Session, Customer);

            SessionUser = SessionUser.GetSession(HttpContext.Session);

            if (SessionUser.CustomerID != 0)
            {
                if (SessionUser.ProfileID != 0)
                {
                    orderService.UpdateProfile(HttpContext.Session, Customer);
                }
                orderService.PlaceOrder(HttpContext.Session, Cart, CustomerAdress, Payment);
            }

            return RedirectToPage("/Index");
        }

    }
}
