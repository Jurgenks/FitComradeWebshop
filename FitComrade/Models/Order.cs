using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitComrade.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerID { get; set; }
        public string OrderStatus { get; set; }
        public decimal OrderPrice { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        
    }
}
