using System.Linq;
using FitComrade.Data;
using FitComrade.Domain.Entities;
using FitComrade.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Core.Services;

namespace FitComrade.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly IDataService _service;

        public IndexModel(IDataService service)
        {
            _service = service;
        }

        public SessionUser SessionUser = new SessionUser();

        public Customer Customer { get; set; }

        public Credit Credits { get; set; }

        public void OnGet()
        {
            SessionUser = SessionUser.GetSession(HttpContext.Session);

            if (SessionUser.ProfileID != 0)
            {
                Customer = _service.GetCustomers().FirstOrDefault(item => item.CustomerID.Equals(SessionUser.ProfileID));

                Credits = _service.GetCredits().FirstOrDefault(item => item.CustomerID.Equals(SessionUser.ProfileID));
            }

        }

    }
}
