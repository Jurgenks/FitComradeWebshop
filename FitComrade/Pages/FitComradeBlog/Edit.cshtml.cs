using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Models;
using Microsoft.EntityFrameworkCore;
using FitComrade.Domain.Entities;
using FitComrade.Data;
using FitComrade.Core.Services;
using FitComrade.Helpers;

namespace FitComrade.Pages.FitComradeBlog
{
    public class EditModel : PageModel
    {
        private readonly FitComradeContext _context;
        public EditModel(FitComradeContext context)
        {
            _context = context;
        }
        public Blog Blog { get; private set; }
        public SessionUser SessionUser = new SessionUser();

        [BindProperty]
        public Workout Workout { get; set; }

        public void OnGet()
        {
            SessionUser = SessionUser.GetSession(HttpContext.Session);
            if(SessionUser.ProfileID == 0)
            {
                Response.Redirect("/");
            }
            //Haalt de blog op van de ingelogde gebruiker
            Blog = _context.Blogs.Where(item => item.CustomerID.Equals(SessionUser.ProfileID)).FirstOrDefault();
        }
        public IActionResult OnGetCreateBlog()
        {
            BlogService blogController = new BlogService(_context);

            Blog = blogController.CreateBlog(HttpContext.Session);

            return RedirectToPage("Edit");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            SessionUser = SessionUser.GetSession(HttpContext.Session);

            var oldWorkout = SessionHelper.GetObjectFromJson<Workout>(HttpContext.Session, "workout");

            if (oldWorkout != null)  //Bestaande Workout
            {
                try
                {                    
                    Blog = _context.Blogs.Where(b => b.Workouts.Contains(oldWorkout)).FirstOrDefault();
                    //Update Workout changes
                    oldWorkout.WorkoutImage = Workout.WorkoutImage;
                    oldWorkout.WorkoutName = Workout.WorkoutName;
                    oldWorkout.WorkoutVideo = Workout.WorkoutVideo;
                    oldWorkout.Discription = Workout.Discription;
                    oldWorkout.Confirmed = Workout.Confirmed;

                    BlogService blogController = new BlogService(_context);

                    await blogController.AddWorkoutToBlogAsync(Blog.BlogID, oldWorkout);
                }
                catch
                {
                    Response.Redirect("/");
                }

            }
            else  //Nieuwe Workout
            {
                Blog = _context.Blogs.Where(b => b.CustomerID.Equals(SessionUser.ProfileID)).FirstOrDefault();

                BlogService blogController = new BlogService(_context);

                await blogController.AddWorkoutToBlogAsync(Blog.BlogID, Workout);
            }

            
            
            return RedirectToPage("Index");
        }
        
        public async Task<IActionResult> OnGetEditAsync(int? id)
        {
            OnGet();

            if (id == null)
            {
                return NotFound();
            }

            Workout = await _context.Workouts.FirstOrDefaultAsync(w => w.WorkoutID == id);

            SessionHelper.SetObjectAsJson(HttpContext.Session, "workout", Workout);

            if (Workout == null)
            {
                return NotFound();
            }
            return Page();
        }

    }
}
