using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FitComrade.Data;
using FitComrade.Models;

namespace FitComrade.Pages.Account.ProductManager
{
    public class CreateModel : PageModel
    {
        private readonly FitComrade.Data.FitComradeContext _context;

        public CreateModel(FitComrade.Data.FitComradeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            SessionUser user = new SessionUser();
            user = user.GetSession(HttpContext.Session,user);

            if(user.ProfileID == 1)
            {
                return Page();
            }
            else
            {
                return NotFound();
            }
            
        }

        [BindProperty]
        public Product Products { get; set; }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Products.Add(Products);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
