using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Models;
using FitComrade.Domain.Entities;
using FitComrade.Core.Services;

namespace FitComrade.Pages.Account.ProductManager
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _service;

        public CreateModel(IProductService service)
        {
            _service = service;
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
        public Product Products { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _service.AddProduct(Products);

            return RedirectToPage("./Index");
        }
    }
}
