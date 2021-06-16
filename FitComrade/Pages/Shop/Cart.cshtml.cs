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
using FitComrade.Core.Services;

namespace FitComrade.Pages.Shop
{
    public class CartModel : PageModel
    {
        private readonly IDataService _service;

        public CartModel(IDataService service)
        {
            _service = service;
        }

        private List<Product> products;
        private CartService cartService = new CartService();

        public Cart Cart = new Cart();
        public decimal Total { get; private set; }  //Totaalprijs van Cart

        public void OnGet()
        {
            Cart.Products = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "cart");

            if (Cart.Products != null)
            {
                Total = Cart.Total();
            }

        }

        public IActionResult OnGetBuyNow(int id)
        {
            products = _service.GetProducts();

            var product = products.Where(item => item.ProductID.Equals(id)).FirstOrDefault();

            Cart.Products = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "cart");


            if (product == null || product.ProductQuantity == 0)
            {
                return NotFound();
            }

            if (Cart.Products == null)
            {
                Cart.Products = cartService.NewCart(product);

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart.Products);
            }
            else
            {
                Cart.Products = cartService.AddToCart(Cart.Products, product);

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart.Products);
            }
            return RedirectToPage("Cart");
        }

        public IActionResult OnGetDelete(int id)
        {
            Cart.Products = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "cart");

            Cart.Products = cartService.RemoveFromCart(Cart.Products, id);

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart.Products);

            return RedirectToPage("Cart");
        }

        public IActionResult OnPostUpdate(int[] quantities)
        {
            Cart.Products = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "cart");

            if (Cart.Products != null)
            {
                Cart.Products = cartService.UpdateCart(Cart.Products, quantities);

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart.Products);
            }

            return RedirectToPage("Cart");
        }


    }
}
