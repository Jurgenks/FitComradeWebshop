using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Models;
using FitComrade.Domain.Entities;
using FitComrade.Data;
using FitComrade.Core.Services;

namespace FitComrade.Pages.Account.Profits
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

        public List<Product> Products;
        public List<OrderDetail> OrderDetails;
        public List<Order> Orders;
        private SessionUser sessionUser = new SessionUser();

        public decimal sale, purchase, profit;
        public decimal DaySale, MonthSale, YearSale;     
        

        public void OnGet()
        {
            sessionUser = sessionUser.GetSession(HttpContext.Session);
            if(sessionUser.ProfileID != 1)
            {
                Response.Redirect("/");
            }

            Products = _service.GetProducts();

            Orders = _service.GetOrders().Where(o => o.OrderStatus != "Dismissed").ToList();

            OrderDetails = _service.GetOrderDetails().Where(o=>o.Order.OrderStatus != "Dismissed").ToList();

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
                foreach (var item in OrderDetails)
                {
                    purchase -= ((decimal)0.60 * item.TotalPrice);
                }
                profit = purchase + sale;
            }            

            DaySale = _orderService.GetSales(DateTime.Today, "Day");

            MonthSale = _orderService.GetSales(DateTime.Today, "Month");

            YearSale = _orderService.GetSales(DateTime.Today, "Year");

        }
        

    }
}
