using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitComrade.Core.Services;
using FitComrade.Data;
using FitComrade.Domain.Entities;
using FitComrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitComrade.Pages.Account.Credits
{
    public class CreateModel : PageModel
    {
        private readonly ICreditService _service;

        public CreateModel(ICreditService service)
        {
            _service = service;
        }

        [BindProperty]
        public CreditCode CreditCode { get; set; }

        private SessionUser sessionUser = new SessionUser();

        public void OnGet()
        {
            sessionUser = sessionUser.GetSession(HttpContext.Session);
            if (sessionUser.ProfileID != 1)
            {
                Response.Redirect("/");
            }

        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool created = _service.CreateCode(HttpContext.Session, CreditCode);

            if (created == true)
            {
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
