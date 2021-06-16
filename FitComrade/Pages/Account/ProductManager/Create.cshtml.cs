using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FitComrade.Data;
using FitComrade.Models;
using FitComrade.Domain.Entities;
using FitComrade.Core.Services;
using FitComrade.Core.Controllers;

namespace FitComrade.Pages.Account.ProductManager
{
    public class CreateModel : PageModel
    {
        private readonly FitComradeContext _context;

        public CreateModel(FitComradeContext context)
        {
            _context = context;
        }

        private SessionUser sessionUser = new SessionUser();

        public IActionResult OnGet()
        {
            sessionUser = sessionUser.GetSession(HttpContext.Session);

            if (sessionUser.ProfileID == 1)
            {
                return Page();
            }
            else
            {
                return NotFound();
            }

        }

        [BindProperty]
        public Product Products { get; private set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ProductController productController = new ProductController(_context);

            productController.AddProduct(Products);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
