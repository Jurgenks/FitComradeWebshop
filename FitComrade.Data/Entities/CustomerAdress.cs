using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FitComrade.Data.Entities
{
    public class CustomerAdress
    {
        public int CustomerAdressID { get; set; }

        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }
        
        public string Adress { get; set; }
    }
}
