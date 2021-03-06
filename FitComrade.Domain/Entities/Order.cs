using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitComrade.Domain.Entities
{
    public class Order
    {
        public int OrderID { get; private set; }

        public DateTime OrderDate { get; set; }

        public int CustomerID { get; set; }

        public string OrderStatus { get; set; }

        public decimal OrderPrice { get; set; }      
        
        public int PaymentID { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public CustomerAdress CustomerAdress { get; set; }

        public virtual Payment Payment { get; set; }
    }
}
