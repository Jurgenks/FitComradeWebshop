﻿using System.Collections.Generic;
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
        [DataType(DataType.PostalCode)]
        public string CustomerPostalCode { get; set; }

        public string CustomerAdress { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string CustomerPhone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string CustomerEmail { get; set; }

        public string Payment { get; set; }

        public string Bank { get; set; }   

        public List<Order> Orders { get; set; }
        
    }
}
