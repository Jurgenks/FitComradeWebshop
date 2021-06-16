using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Domain.Entities;
using FitComrade.Data;
using FitComrade.Core.Services;

namespace FitComrade.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly IDataService _service;
        
        public IndexModel(IDataService service)
        {
            _service = service;
        }

        public List<Product> Products { get; private set; }        

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public void OnGet()
        {
            Products = _service.GetProducts().OrderByDescending(item => item.ProductQuantity).ToList();

            if (!string.IsNullOrEmpty(SearchString))
            {
                Products = _service.GetProducts().Where(item => item.ProductName.Contains(SearchString)).ToList();
            }
        }

        public IActionResult OnGetSort(int id)
        {
            if (id == 1)
            {
                Products = _service.GetProducts().OrderBy(item => item.ProductID).ToList();
            }
            if (id == 2)
            {
                Products = _service.GetProducts().OrderBy(item => item.ProductName).ToList();
            }
            if (id == 3)
            {
                Products = _service.GetProducts().OrderBy(item => item.ProductPrice).ToList();
            }
            if (id == 4)
            {
                Products = _service.GetProducts().OrderByDescending(item => item.ProductQuantity).ToList();
            }

            return Page();
        }

    }
}
