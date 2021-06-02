using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitComrade.Data;
using FitComrade.Models;
using FitComrade.Domain.Entities;
using FitComrade.Core.Controller;

namespace FitComrade.Pages.Account.ProductManager
{
    public class DeleteModel : PageModel
    {
        private readonly FitComradeContext _context;

        public DeleteModel(FitComradeContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductController productController = new ProductController(_context);
            bool delete = await productController.DeleteProductAsync(id);
            if(delete == false)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
