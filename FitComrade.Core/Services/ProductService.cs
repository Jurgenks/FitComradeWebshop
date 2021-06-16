using FitComrade.Data;
using FitComrade.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FitComrade.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly FitComradeContext _context;

        public ProductService(FitComradeContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product) //Create Product
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public async Task<bool> DeleteProductAsync(int? id) //Delete Product
        {
            var Products = await _context.Products.FindAsync(id);

            var OrderDetails = _context.OrderDetails.Where(product => product.ProductID.Equals(id)).ToList();

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

        public async Task UpdateProductAsync(Product Products) //Update Product
        {

            try
            {
                _context.Attach(Products).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(Products.ProductID))
                {
                    return;
                }
                else
                {
                    throw;
                }
            }
        }

        private bool ProductsExists(int id) //Check if Product Exists
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
