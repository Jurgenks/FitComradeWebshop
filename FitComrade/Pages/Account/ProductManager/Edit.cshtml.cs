using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Domain.Entities;
using FitComrade.Core.Services;
using System.Linq;

namespace FitComrade.Pages.Account.ProductManager
{
    public class EditModel : PageModel
    {
        private readonly IDataService _service;

        private readonly IProductService _productService;

        public EditModel(IDataService service, IProductService productService)
        {
            _service = service;
            _productService = productService;
        }

        [BindProperty]
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


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Products == null)
            {
                return NotFound();
            }

            await _productService.UpdateProductAsync(Products);

            return RedirectToPage("./Index");
        }
        
    }
}
