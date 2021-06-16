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
    public class RedeemModel : PageModel
    {
        private readonly ICreditService _service;

        public RedeemModel(ICreditService service)
        {
            _service = service;
        }

        [BindProperty]
        public CreditCode CreditCode { get; set; }

        private SessionUser sessionUser = new SessionUser();

        public void OnGet()
        {
            sessionUser = sessionUser.GetSession(HttpContext.Session);
            if (sessionUser.ProfileID == 0)
            {
                Response.Redirect("/");
            }

        }

        public IActionResult OnPost()
        {
            if (CreditCode.CreditCodeString != null && CreditCode.CreditCodeString.Length <= 10)
            {
                bool succes = _service.RedeemCode(HttpContext.Session, CreditCode);

                if (succes == true)
                {
                    return RedirectToPage("/Account/Index");
                }
                else
                {
                    return Page();
                }

            }
            else
            {
                return Page();
            }
        }
    }
}
