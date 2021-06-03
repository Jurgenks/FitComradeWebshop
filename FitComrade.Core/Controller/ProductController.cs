using FitComrade.Data;
using FitComrade.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitComrade.Core.Controller
{
    public class ProductController : ControllerBase
    {
        private readonly FitComradeContext _context;

        public ProductController(FitComradeContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product) //Create Product
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public async Task<bool> DeleteProductAsync(int? id)
        {
            var Products = await _context.Products.FindAsync(id);

            var OrderDetails = _context.OrderDetails.Where(product=>product.ProductID.Equals(id)).ToList();

            if (Products != null && OrderDetails.Count == 0)
            {
                _context.Products.Remove(Products);
                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task UpdateProductAsync(Product Products)
        {           

            _context.Attach(Products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(Products.ProductID))
                {
                    NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
