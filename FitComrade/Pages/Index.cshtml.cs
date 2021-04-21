using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using FitComrade.Helpers;

namespace FitComrade.Pages
{
    public class IndexModel : PageModel
    {
        public SessionUser user = new SessionUser();
        
        public void OnGet()
        {
            user = user.GetSession(HttpContext.Session, user);            
        }
        public IActionResult OnGetLogOut()
        {
            user.LogOutSession(HttpContext.Session);
            return Page();
        }




    }
}

