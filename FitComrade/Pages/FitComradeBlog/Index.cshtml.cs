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

        public void OnGet()  //Haalt alle blogs en bijbehordende workouts op
        {
            SessionUser = SessionUser.GetSession(HttpContext.Session);

            HasNoBlog = CanCreateBlog();

            Workouts = _service.GetWorkouts().Where(workout => workout.Confirmed == true).ToList();

            Blogs = _service.GetBlogs().Where(blog => blog.Workouts.Count > 0).ToList();

        }

        public void OnGetCheckNewPosts()
        {
            SessionUser = SessionUser.GetSession(HttpContext.Session);

            var workouts = _service.GetWorkouts().Where(workout => workout.Confirmed == false);

            var blogs = _service.GetBlogs().Where(blog => blog.Workouts.Count > 0);

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
