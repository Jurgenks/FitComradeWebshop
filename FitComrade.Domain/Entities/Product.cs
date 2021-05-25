using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitComrade.Domain.Entities
{
    public class Product
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        [DataType(DataType.Currency)]
        public decimal ProductPrice { get; set; }

        public int ProductQuantity { get; set; }

    }
}
