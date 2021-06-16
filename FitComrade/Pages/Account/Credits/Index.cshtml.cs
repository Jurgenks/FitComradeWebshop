using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitComrade.Data;
using FitComrade.Domain.Entities;
using FitComrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitComrade.Core.Services;

namespace FitComrade.Pages.Account.Credits
{
    public class IndexModel : PageModel
    {
        private readonly IDataService _service;

        public IndexModel(IDataService service)
        {
            _service = service;
        }

        public List<CreditCode> CreditCodes { get; private set; }

        private SessionUser sessionUser = new SessionUser();

        public void OnGet()
        {
            sessionUser = sessionUser.GetSession(HttpContext.Session);

            if (sessionUser.ProfileID != 1)
            {
                Response.Redirect("/");
            }

            CreditCodes = _service.GetCreditCodes();
        }
    }
}
