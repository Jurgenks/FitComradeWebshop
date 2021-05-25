using System;
using System.Collections.Generic;
using System.Text;

namespace FitComrade.Domain.Entities
{
    public class Payment
    {
        public int PaymentID { get; set; }
        
        public string PaymentMethod { get; set; }
    }
}
