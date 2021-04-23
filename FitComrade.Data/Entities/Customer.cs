using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitComrade.Data.Entities
{
    public class Customer
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int CustomerID { get; set; }

        public string CustomerSurName { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPostalCode { get; set; }

        public string CustomerAdress { get; set; }

        public string CustomerPhone { get; set; }

        public string CustomerEmail { get; set; }

        public string Payment { get; set; }

        public string Bank { get; set; }   

        public List<Order> Orders { get; set; }
        
    }
}
