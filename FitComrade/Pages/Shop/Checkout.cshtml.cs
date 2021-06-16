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
        private readonly FitComradeContext _context;

        public CheckoutModel(FitComradeContext context)
        {
            _context = context;
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

            Payments = _context.Payments.ToList();

            if (Cart.Products == null || Cart.Products.Count == 0)
            {
                Response.Redirect("/");
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            OrderService dataController = new OrderService(_context);

            Cart.Products = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "cart");

            dataController.RegisterCustomer(HttpContext.Session, Customer);

            SessionUser = SessionUser.GetSession(HttpContext.Session);

            if (SessionUser.CustomerID != 0)
            {
                if (SessionUser.ProfileID != 0)
                {
                    dataController.UpdateProfile(HttpContext.Session, Customer);
                }
                dataController.PlaceOrder(HttpContext.Session, Cart, CustomerAdress, Payment);
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

    }
}
