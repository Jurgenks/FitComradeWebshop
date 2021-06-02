using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FitComrade.Data;
using FitComrade.Models;
using FitComrade.Domain.Entities;
using FitComrade.Core;
using FitComrade.Core.Controller;

namespace FitComrade.Pages.Account.ProductManager
{
    public class EditModel : PageModel
    {
        private readonly FitComradeContext _context;

        public EditModel(FitComradeContext context)
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

        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ProductController productController = new ProductController(_context);
            await productController.UpdateProductAsync(Products.ProductID);

            return RedirectToPage("./Index");
        }
        
    }
}
