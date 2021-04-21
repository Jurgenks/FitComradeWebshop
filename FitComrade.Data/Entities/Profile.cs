using System.ComponentModel.DataAnnotations;

namespace FitComrade.Data.Entities
{
    public class Profile
    {
        public int ProfileID { get; set; }
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public int CustomerID { get; set; }
        
        
    }
}
