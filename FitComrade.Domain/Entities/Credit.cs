using System;
using System.Collections.Generic;
using System.Text;

namespace FitComrade.Domain.Entities
{
    public class Credit
    {
        public int CreditID { get; set; }

        public decimal CreditValue { get; set; }

        public int CustomerID { get; set; }
    }
}
