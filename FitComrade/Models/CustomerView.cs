using FitComrade.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitComrade.Models
{
    public class CustomerView
    {
        public string UserName { get; set; }        

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
