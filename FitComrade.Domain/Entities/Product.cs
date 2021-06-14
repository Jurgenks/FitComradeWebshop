using System.ComponentModel.DataAnnotations;

namespace FitComrade.Domain.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal ProductPrice { get; set; }
        [Required]
        public int ProductQuantity { get; set; }

        public string ProductImage { get; set; }

    }
}
