using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FitComrade.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FitComrade.Data;

namespace FitComrade.Test
{
    public class ProductService
    {
        private TestContext _context;

        public ProductService(TestContext context)
        {
            _context = context;
        }

        public void AddProduct(string name, decimal price, int stock)
        {
            _context.Products.Add(new Product {ProductName = name, ProductPrice = price, ProductQuantity = stock });
            _context.SaveChanges();
        }

        public List<Product> GetAllProducts()
        {
            var query = from b in _context.Products
                        orderby b.ProductName
                        select b;

            return query.ToList();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var query = from b in _context.Products
                        orderby b.ProductName
                        select b;

            return await query.ToListAsync();
        }

    }
}
