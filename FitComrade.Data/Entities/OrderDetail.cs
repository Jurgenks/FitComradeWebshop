using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitComrade.Data.Entities
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderDetailID { get; set; }
    }
}
