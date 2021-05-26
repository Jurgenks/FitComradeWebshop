using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitComrade.Domain.Entities
{
    public class OrderDetail
    {
        public int OrderID { get; private set; }

        public int ProductID { get; set; }   
        
        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }

        public int OrderDetailID { get; set; }

        public virtual Product Product { get; set; }

        public virtual Order Order { get; set; }
    }
}
