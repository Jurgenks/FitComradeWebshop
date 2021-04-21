using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitComrade.Data;
using FitComrade.Data.Entities;

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
        public Profile Profile;
        public List<Product> Products;
        

        public async Task<IActionResult> OnGetAsync(int? id) // Haalt OrderDetails op met specifiek id
        {
            if (id == null) // Er is geen order met het specifieke id
            {
                return NotFound();
            }

            OrderDetail = _context.OrderDetails.Where(m => m.OrderID.Equals(id)).ToList(); 
            
            Products = _context.Products.ToList(); 

            Order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderID == id);

            Customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerID == Order.CustomerID);

            Profile = await _context.Profiles.FirstOrDefaultAsync(m => m.CustomerID == Order.CustomerID);

            if (Order == null) // Er zijn geen orders
            {
                return NotFound();
            }
            return Page();
        }
    }
}
