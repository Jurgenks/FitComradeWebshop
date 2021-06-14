using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitComrade.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitComrade.Data;
using FitComrade.Domain.Entities;

namespace FitComrade.Pages.FitComradeBlog
{
    public class IndexModel : PageModel
    {
        private readonly FitComradeContext _context;

        public IndexModel(FitComradeContext context)
        {
            _context = context;
        }

        public SessionUser SessionUser = new SessionUser();
        public List<Blog> Blogs { get; private set; }
        public List<Workout> Workouts { get; private set; }
        public bool HasNoBlog { get; private set; }

        public async Task OnGetAsync()  //Haalt alle blogs en bijbehordende workouts op
        {
            SessionUser = SessionUser.GetSession(HttpContext.Session);

            HasNoBlog = CanCreateBlog();

            var blogs = _context.Blogs.Where(blog => blog.Workouts.Count > 0);

            var workouts = _context.Workouts.Where(workout => workout.Confirmed == true);

            if (blogs.Count() > 0 && workouts.Count() > 0)
            {
                Workouts = await workouts.ToListAsync();
                Blogs = await blogs.ToListAsync();
            }


        }

        private bool CanCreateBlog()    //Check of de gebruiker al een blog heeft
        {
            var ProfileBlogs = _context.Blogs.Where(item => item.CustomerID.Equals(SessionUser.ProfileID));
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

        public async Task OnGetCheckNewPostsAsync()
        {
            SessionUser = SessionUser.GetSession(HttpContext.Session);

            var blogs = _context.Blogs.Where(blog => blog.Workouts.Count > 0);

            var workouts = _context.Workouts.Where(workout => workout.Confirmed == false);

            if (blogs.Count() > 0 && workouts.Count() > 0)
            {
                Workouts = await workouts.ToListAsync();
                Blogs = await blogs.ToListAsync();
            }
        }
    }
}
