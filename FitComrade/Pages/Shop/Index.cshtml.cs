using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Domain.Entities;
using FitComrade.Data;

namespace FitComrade.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly FitComradeContext _context;

        public IndexModel(FitComradeContext context)
        {
            _context = context;
        }

        public List<Product> Products { get; private set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

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
