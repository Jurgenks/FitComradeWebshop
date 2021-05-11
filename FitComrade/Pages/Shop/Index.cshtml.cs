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
            Products = _context.Products.ToList();

            if (!string.IsNullOrEmpty(SearchString))
            {
                Products = _context.Products.Where(item=>item.ProductName.Contains(SearchString)).ToList();
            }
        }
        
    }
}
