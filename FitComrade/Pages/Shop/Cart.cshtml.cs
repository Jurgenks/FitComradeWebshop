using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Models;
using FitComrade.Helpers;
using FitComrade.Domain.Entities;
using FitComrade.Data;
using FitComrade.Core.Controller;

namespace FitComrade.Pages.Shop
{
    public class CartModel : PageModel
    {
        private readonly FitComradeContext _context;

        public CartModel(FitComradeContext context)
        {
            _context = context;
        }

        public Cart Cart = new Cart();
        private List<Product> Products;
        
        public decimal Total { get; private set; }  //Totaalprijs van Cart

        public void OnGet()
        {
            Cart.Products = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "cart");

            if(Cart.Products != null)
            {
                Total = Cart.Total();
            }
            
        }

        public IActionResult OnGetBuyNow(int id)
        {
            Products = _context.Products.ToList();

            var product = Products.Where(item=>item.ProductID.Equals(id)).FirstOrDefault();
            
            Cart.Products = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "cart");

            CartController cartController = new CartController();

            if (Cart.Products == null)
            {
                Cart.Products = cartController.NewCart(product);

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart.Products);
            }
            else
            {
                Cart.Products = cartController.AddToCart(Cart.Products, product);

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart.Products);
            }
            return RedirectToPage("Cart");
        }

        public IActionResult OnGetDelete(int id)
        {
            Cart.Products = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "cart");

            CartController cartController = new CartController();

            Cart.Products = cartController.RemoveFromCart(Cart.Products, id);

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart.Products);

            return RedirectToPage("Cart");
        }

        public IActionResult OnPostUpdate(int[] quantities)
        {
            Cart.Products = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "cart");

            if(Cart.Products != null)
            {
                CartController cartController = new CartController();

                Cart.Products = cartController.UpdateCart(Cart.Products, quantities);

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart.Products);
            }           

            return RedirectToPage("Cart");
        }

        
    }
}
