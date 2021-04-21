using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitComrade.Data.Entities
{
    public class Customer
    {
        public int CustomerID { get; set; }
        [Required]
        public string CustomerSurName { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerPostalCode { get; set; }
        [Required]
        public string CustomerAdress { get; set; }
        [Required]
        public string CustomerPhone { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public string Payment { get; set; }
        [Required]
        public string Bank { get; set; }   
        public List<Order> Orders { get; set; }
        
    }
}
