using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitComrade.Data;
using FitComrade.Domain.Entities;
using FitComrade.Models;

namespace FitComrade.Pages.Account.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly FitComradeContext _context;

        public DetailsModel(FitComradeContext context)
        {
            _context = context;
        }

        public Order Order;
        public List<OrderDetail> OrderDetail;
        public Customer Customer;
        public List<Payment> Payments;
        public List<CustomerAdress> CustomerAdress;
        public List<Product> Products;
        private SessionUser user = new SessionUser();
        

        public async Task<IActionResult> OnGetAsync(int? id) // Haalt OrderDetails op met specifiek id
        {
            if (id == null) // Er is geen order met het specifieke id
            {
                return NotFound();
            }

            user = user.GetSession(HttpContext.Session, user);           

            OrderDetail = await _context.OrderDetails.Where(m => m.OrderID.Equals(id)).ToListAsync(); 
            
            Products = await _context.Products.ToListAsync(); 

            Order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderID == id);

            if (Order == null || user.ProfileID != Order.CustomerID && user.ProfileID != 1) // Er zijn geen orders of gebruiker heeft geen toegang
            {
                return NotFound();
            }

            Customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerID == Order.CustomerID);

            Payments = await _context.Payments.ToListAsync();

            CustomerAdress = await _context.CustomerAdresses.ToListAsync();

            
            return Page();
        }
    }
}
