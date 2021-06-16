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

namespace FitComrade.Pages.Account.ProductManager
{
    public class IndexModel : PageModel
    {
        private readonly IDataService _service;

        public IndexModel(IDataService service)
        {
            _service = service;
        }

        private SessionUser sessionUser = new SessionUser();

        public IList<Product> Products { get; private set; }

        public List<OrderDetail> OrderDetails { get; private set; }

        public void OnGet()
        {
            sessionUser = sessionUser.GetSession(HttpContext.Session);

            if (sessionUser.ProfileID == 1)
            {
                Products = _service.GetProducts();

                OrderDetails = _service.GetOrderDetails();
            }
            else
            {
                Response.Redirect("/");
            }

        }


    }
}
