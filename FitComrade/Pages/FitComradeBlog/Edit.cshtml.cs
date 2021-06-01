using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Models;
using Microsoft.EntityFrameworkCore;
using FitComrade.Domain.Entities;
using FitComrade.Data;
using FitComrade.Core;
using FitComrade.Core.Controller;
using FitComrade.Helpers;

namespace FitComrade.Pages.FitComradeBlog
{
    public class EditModel : PageModel
    {
        private readonly Data.FitComradeContext _context;
        public EditModel(Data.FitComradeContext context)
        {
            _context = context;
        }
        public Blog Blog { get; set; }
        public SessionUser user = new SessionUser();

        [BindProperty]
        public Workout Workout { get; set; }

        public void OnGet()
        {
            user = user.GetSession(HttpContext.Session, user);
            if(user.ProfileID == 0)
            {
                Response.Redirect("/");
            }
            //Haalt de blog op van de ingelogde gebruiker
            Blog = _context.Blogs.Where(item => item.CustomerID.Equals(user.ProfileID)).FirstOrDefault();
        }
        public IActionResult OnGetCreateBlog()
        {
            BlogController blogController = new BlogController(_context);

            Blog = blogController.CreateBlog(HttpContext.Session);

            return RedirectToPage("Edit");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            user = user.GetSession(HttpContext.Session, user);

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

                    BlogController blogController = new BlogController(_context);

                    await blogController.AddWorkoutToBlogAsync(Blog.BlogID, oldWorkout);
                }
                catch
                {
                    Response.Redirect("/");
                }

            }
            else  //Nieuwe Workout
            {
                Blog = _context.Blogs.Where(b => b.CustomerID.Equals(user.ProfileID)).FirstOrDefault();

                BlogController blogController = new BlogController(_context);

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
