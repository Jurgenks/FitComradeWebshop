using FitComrade.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FitComrade.Core.Services
{
    public interface IProductService
    {
        void AddProduct(Product product);
        Task<bool> DeleteProductAsync(int? id);
        Task UpdateProductAsync(Product Products);
    }
}
