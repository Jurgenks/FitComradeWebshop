using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitComrade.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitComrade.Data;
using FitComrade.Data.Entities;

namespace FitComrade.Pages.FitComradeBlog
{
    public class IndexModel : PageModel
    {
        private readonly FitComradeContext _context;

        public IndexModel(FitComradeContext context)
        {
            _context = context;
        }
        public SessionUser user = new SessionUser();
        public List<Blog> Blogs { get; set; } 
        public List<Workout> Workouts { get; set; }
        public bool HasNoBlog { get; private set; }

        public async Task OnGetAsync()  //Haalt alle blogs en bijbehordende workouts op
        {            
            user = user.GetSession(HttpContext.Session, user);

            HasNoBlog = CanCreateBlog();

            var blogs = _context.Blogs.Where(blog => blog.Workouts.Count > 0);

            var workouts = _context.Workouts.Where(workout=>workout.Confirmed == true);

            if (blogs.Count() > 0 && workouts.Count() > 0)
            {
                Workouts = await workouts.ToListAsync();
                Blogs = await blogs.ToListAsync();                
            }
            
            
        }
        private bool CanCreateBlog()    //Check of de gebruiker al een blog heeft
        {
            var ProfileBlogs = _context.Blogs.Where(item => item.ProfileID.Equals(user.ProfileID));
            if(user.ProfileID != 0)
            {
                if(ProfileBlogs.Count() > 0)
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
            user = user.GetSession(HttpContext.Session, user);

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
