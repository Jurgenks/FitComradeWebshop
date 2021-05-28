using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitComrade.Domain.Entities
{
    public class Cart
    {
        public string CartID { get; set; }
        public List<Product> Products { get; set; }

        public decimal Total()  //Ontvang alle prijzen van de List<Product>
        {
            decimal Total = 0;
            foreach (var item in Products.Where(item => item != null))
            {
                decimal itemTotal = item.ProductPrice * item.ProductQuantity;
                Total += itemTotal;
            }
            return Total;
        }
        
        
    }
}
