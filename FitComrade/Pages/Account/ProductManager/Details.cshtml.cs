using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitComrade.Data;
using FitComrade.Domain.Entities;

namespace FitComrade.Pages.Account.ProductManager
{
    public class DetailsModel : PageModel
    {
        private readonly FitComradeContext _context;

        public DetailsModel(FitComradeContext context)
        {
            _context = context;
        }

        public Product Products { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Products = await _context.Products.FirstOrDefaultAsync(m => m.ProductID == id);

            if (Products == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
