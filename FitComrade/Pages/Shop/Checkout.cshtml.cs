using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Models;
using FitComrade.Helpers;
using FitComrade.Data.Entities;
using FitComrade.Data;

namespace FitComrade.Pages.Shop
{
    public class CheckoutModel : PageModel
    {
        private readonly Data.FitComradeContext _context;

        public CheckoutModel(Data.FitComradeContext context)
        {
            _context = context;
        }

        public Cart Cart = new Cart();        

        [BindProperty]
        public Customer Customer { get; set; }        
        

        public void OnGet()
        {
            Cart.Products = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "cart");
            
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart.Products);

            if(Cart.Products == null || Cart.Products.Count == 0)
            {
                Response.Redirect("/");
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            SessionUser user = new SessionUser();

            DataController dataController = new DataController(_context);

            Cart.Products = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "cart");          

            dataController.RegisterCustomer(HttpContext.Session, Customer);

            user = user.GetSession(HttpContext.Session, user);
            
            if(user.CustomerID != 0)
            {
                if (user.ProfileID != 0)
                {
                    dataController.UpdateProfile(HttpContext.Session, Customer);
                }
                dataController.PlaceOrder(user.CustomerID, Cart);
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
        
    }
}
