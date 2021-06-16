using System.Linq;
using FitComrade.Core.Services;
using FitComrade.Domain.Entities;
using FitComrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitComrade.Pages.Account.Credits
{
    public class DeleteModel : PageModel
    {
        private readonly IDataService _service;
        private readonly ICreditService _creditService;

        public DeleteModel(IDataService service, ICreditService creditService)
        {
            _service = service;
            _creditService = creditService;
        }

        [BindProperty]
        public CreditCode CreditCode { get; set; }

        private SessionUser sessionUser = new SessionUser();


        public IActionResult OnGet(int? id)
        {
            sessionUser = sessionUser.GetSession(HttpContext.Session);
            if (sessionUser.ProfileID != 1)
            {
                Response.Redirect("/");
            }

            if (id == null)
            {
                return NotFound();
            }

            CreditCode = _service.GetCreditCodes().FirstOrDefault(m => m.CreditCodeID == id);

            if (CreditCode == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CreditCode = _service.GetCreditCodes().FirstOrDefault(c => c.CreditCodeID.Equals(id));

            if (CreditCode != null && CreditCode.CreditIsValid == true)
            {
                _creditService.DeleteCode(HttpContext.Session, CreditCode);
            }

            return RedirectToPage("./Index");
        }
    }
}
