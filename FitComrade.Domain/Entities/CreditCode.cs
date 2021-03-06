using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FitComrade.Domain.Entities
{
    public class CreditCode
    {
        public int CreditCodeID { get; private set; }

        [DataType(DataType.Currency)]
        public decimal CreditValue { get; set; }

        public string CreditCodeString { get; set; }

        public bool CreditIsValid { get; set; }
    }
}
