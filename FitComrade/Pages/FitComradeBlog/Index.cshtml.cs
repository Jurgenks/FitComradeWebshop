using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitComrade.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitComrade.Data;
using FitComrade.Domain.Entities;
using FitComrade.Core.Services;

namespace FitComrade.Pages.FitComradeBlog
{
    public class IndexModel : PageModel
    {
        private readonly IDataService _service;

        public IndexModel(IDataService service)
        {
            _service = service;
        }

        public SessionUser SessionUser = new SessionUser();

        public List<Blog> Blogs { get; private set; }
        public List<Workout> Workouts { get; private set; }

        public bool HasNoBlog { get; private set; }

        public async Task OnGetAsync()  //Haalt alle blogs en bijbehordende workouts op
        {
            SessionUser = SessionUser.GetSession(HttpContext.Session);

            HasNoBlog = CanCreateBlog();

            Blogs = await _service.GetBlogsAsync(0);

            Workouts = await _service.GetWorkoutsAsync(true);
        }

        public async Task OnGetCheckNewPostsAsync()
        {
            SessionUser = SessionUser.GetSession(HttpContext.Session);

            Blogs = await _service.GetBlogsAsync(0);

            Workouts = await _service.GetWorkoutsAsync(false);

        }

        public async Task OnGetProfileBlog()
        {
            SessionUser = SessionUser.GetSession(HttpContext.Session);

            Blogs = await _service.GetBlogsAsync(SessionUser.CustomerID);

            Workouts = await _service.GetWorkoutsAsync(false);

        }

        private bool CanCreateBlog()    //Check of de gebruiker al een blog heeft
        {
            var ProfileBlogs = _service.GetBlogs().Where(item => item.CustomerID.Equals(SessionUser.ProfileID));
            if (SessionUser.ProfileID != 0)
            {
                if (ProfileBlogs.Count() > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
    }
}
