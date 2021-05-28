using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FitComrade.Domain.Entities
{
    public class Credit
    {
        public int CreditID { get; private set; }

        [DataType(DataType.Currency)]
        public decimal CreditValue { get; set; }

        public int CustomerID { get; set; }
    }
}
