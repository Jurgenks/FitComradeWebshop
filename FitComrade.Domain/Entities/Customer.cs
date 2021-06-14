using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitComrade.Domain.Entities
{
    public class Customer
    {

        public int CustomerID { get; private set; }

        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string CustomerSurName { get; set; }

        public string CustomerName { get; set; }    
        
        [DataType(DataType.PhoneNumber)]
        public string CustomerPhone { get; set; }

        [DataType(DataType.EmailAddress)]
        public string CustomerEmail { get; set; }

        public List<Order> Orders { get; set; }

        public Credit Credit { get; set; }

        public Blog Blog { get; set; }
        
    }
}
