using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Domain.Entities;
using FitComrade.Core.Services;
using System.Linq;

namespace FitComrade.Pages.Account.ProductManager
{
    public class DetailsModel : PageModel
    {
        private readonly IDataService _service;

        public DetailsModel(IDataService service)
        {
            _service = service;
        }

        public Product Products { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Products = _service.GetProducts().FirstOrDefault(m => m.ProductID == id);

            if (Products == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
