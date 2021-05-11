using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Models;
using FitComrade.Helpers;
using FitComrade.Domain.Entities;

namespace FitComrade.Pages.Shop
{
    public class CartModel : PageModel
    {
        private readonly Data.FitComradeContext _context;

        public CartModel(Data.FitComradeContext context)
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

            if (Cart.Products == null)
            {                
                Cart.Products = new List<Product>();
                Cart.Products.Add(new Product
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    ProductQuantity = 1,
                    ProductPrice = product.ProductPrice
                });

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart.Products);
            }
            else
            {
                int index = Exists(Cart.Products, product.ProductID);
                if (index == -1)
                {
                    Cart.Products.Add(new Product
                    {
                        ProductID = product.ProductID,
                        ProductName = product.ProductName,
                        ProductQuantity = 1,
                        ProductPrice = product.ProductPrice
                    });
                }
                else
                {
                    Cart.Products[index].ProductQuantity++;
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart.Products);
            }
            return RedirectToPage("Cart");
        }

        public IActionResult OnGetDelete(int id)
        {
            Cart.Products = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "cart");

            int index = Exists(Cart.Products, id);

            Cart.Products.RemoveAt(index);

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart.Products);

            return RedirectToPage("Cart");
        }

        public IActionResult OnPostUpdate(int[] quantities)
        {
            Cart.Products = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, "cart");

            if(Cart.Products != null)
            {
                for (var i = 0; i < Cart.Products.Count; i++)
                {
                    Cart.Products[i].ProductQuantity = quantities[i];
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", Cart.Products);
            }           

            return RedirectToPage("Cart");
        }

        private int Exists(List<Product> cart, int id)
        {
            for (var i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductID == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
