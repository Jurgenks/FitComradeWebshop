using System.ComponentModel.DataAnnotations;

namespace FitComrade.Domain.Entities
{
    public class Product
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        [DataType(DataType.Currency)]
        public decimal ProductPrice { get; set; }

        public int ProductQuantity { get; set; }

        public string ProductImage { get; set; }

    }
}
