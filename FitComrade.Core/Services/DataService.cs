using FitComrade.Data;
using FitComrade.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FitComrade.Core.Services
{
    public class DataService : IDataService
    {
        private readonly FitComradeContext _context;

        public DataService(FitComradeContext context)
        {
            _context = context;
        }

        public List<Product> GetProducts()
        {
            var products = _context.Products.ToList();

            if(products != null)
            {
                return products;
            }
            else
            {
                return null;
            }
        }
    }
}
