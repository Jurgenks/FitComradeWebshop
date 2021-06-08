using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using FitComrade.Domain.Entities;

namespace FitComrade.Pages.Shop
{
    public class IndexModel : PageModel
    {
        public List<Product> Products = new List<Product>();
        private readonly Data.FitComradeContext _context;
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public IndexModel(Data.FitComradeContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Products = _context.Products.OrderByDescending(item => item.ProductQuantity).ToList();

            if (!string.IsNullOrEmpty(SearchString))
            {
                Products = _context.Products.Where(item => item.ProductName.Contains(SearchString)).ToList();
            }
        }

        public IActionResult OnGetSort(int id)
        {
            if (id == 1)
            {
                Products = _context.Products.OrderBy(item => item.ProductID).ToList();
            }
            if (id == 2)
            {
                Products = _context.Products.OrderBy(item => item.ProductName).ToList();
            }
            if (id == 3)
            {
                Products = _context.Products.OrderBy(item => item.ProductPrice).ToList();
            }
            if (id == 4)
            {
                Products = _context.Products.OrderByDescending(item => item.ProductQuantity).ToList();
            }

            return Page();
        }

    }
}
