using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Models;
using FitComrade.Controller;

namespace FitComrade.Pages.Account.Profits
{
    public class IndexModel : PageModel
    {
        private readonly Data.FitComradeContext _context;

        public IndexModel(Data.FitComradeContext context)
        {
            _context = context;
        }

        public List<Product> Products;
        public List<OrderDetail> OrderDetails;
        public List<Order> Orders;
        public SessionUser user = new SessionUser();
        public decimal sale, purchase, profit;
        public decimal DaySale, MonthSale, YearSale;     
        

        public void OnGet()
        {
            user = user.GetSession(HttpContext.Session, user);
            if(user.ProfileID != 1)
            {
                Response.Redirect("/");
            }
            Products = _context.Products.ToList();

            OrderDetails = _context.OrderDetails.ToList();

            Orders = _context.Orders.ToList();

            if(OrderDetails != null && Products != null)
            {
                foreach (var item in OrderDetails)
                {
                    sale += item.TotalPrice;
                }
                foreach (var item in Products)
                {
                    purchase -= ((decimal)0.60 * item.ProductPrice) * item.ProductQuantity;
                }
                profit = purchase + sale;
            }
            
            DataController dataController = new DataController(_context);

            DaySale = dataController.GetSales(DateTime.Today, "Day");

            MonthSale = dataController.GetSales(DateTime.Today, "Month");

            YearSale = dataController.GetSales(DateTime.Today, "Year");

        }
        

    }
}
