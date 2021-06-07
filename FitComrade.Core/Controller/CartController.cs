using FitComrade.Data;
using FitComrade.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitComrade.Core.Controller
{
    public class CartController
    {       

        public List<Product> NewCart(Product product)
        {
            List<Product> Products = new List<Product>();
            Products.Add(new Product
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                ProductQuantity = 1,
                ProductPrice = product.ProductPrice,
                ProductImage = product.ProductImage
            });
            return Products;
        }

        public List<Product> AddToCart(List<Product> Products,Product product)
        {
            int index = Exists(Products, product.ProductID);

            if (index == -1)
            {
                Products.Add(new Product
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    ProductQuantity = 1,
                    ProductPrice = product.ProductPrice,
                    ProductImage = product.ProductImage
                });
            }
            else
            {
                Products[index].ProductQuantity++;
            }
            return Products;
        }
        public List<Product> RemoveFromCart(List<Product> Products, int id)
        {
            int index = Exists(Products, id);

            Products.RemoveAt(index);

            return Products;
        }

        public List<Product> UpdateCart(List<Product> Products, int[] quantities)
        {
            for (var i = 0; i < Products.Count; i++)
            {
                Products[i].ProductQuantity = quantities[i];
            }
            return Products;
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
