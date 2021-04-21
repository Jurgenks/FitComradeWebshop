using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitComrade.Helpers;
using FitComrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitComrade.Pages.Account
{
    public class IndexModel : PageModel
    {
        public SessionUser user = new SessionUser();
        public void OnGet()
        {
            user = user.GetSession(HttpContext.Session, user);
        }
        
    }
}
