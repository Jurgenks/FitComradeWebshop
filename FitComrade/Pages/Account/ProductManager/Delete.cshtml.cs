using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitComrade.Data;
using FitComrade.Domain.Entities;
using FitComrade.Core.Services;
using System.Linq;

namespace FitComrade.Pages.Account.ProductManager
{
    public class DeleteModel : PageModel
    {
        private readonly IDataService _service;

        private readonly IProductService _productService;

        public DeleteModel(IDataService service, IProductService productService)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            bool delete = await _productService.DeleteProductAsync(id);

            if (delete == false)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
