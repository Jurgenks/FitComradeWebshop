using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Models;
using FitComrade.Domain.Entities;
using FitComrade.Core.Services;
using FitComrade.Helpers;

namespace FitComrade.Pages.FitComradeBlog
{
    public class EditModel : PageModel
    {
        private readonly IDataService _service;
        private readonly IBlogService _blogService;

        public EditModel(IDataService service, IBlogService blogService)
        {
            _service = service;
            _blogService = blogService;
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
            Blog = _service.GetBlogs().Where(item => item.CustomerID.Equals(SessionUser.ProfileID)).FirstOrDefault();
        }
        public IActionResult OnGetCreateBlog()
        {
            Blog = _blogService.CreateBlog(HttpContext.Session);

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
                    Blog = _service.GetBlogs().Where(b => b.Workouts.Contains(oldWorkout)).FirstOrDefault();
                    //Update Workout changes
                    oldWorkout.WorkoutImage = Workout.WorkoutImage;
                    oldWorkout.WorkoutName = Workout.WorkoutName;
                    oldWorkout.WorkoutVideo = Workout.WorkoutVideo;
                    oldWorkout.Discription = Workout.Discription;
                    oldWorkout.Confirmed = Workout.Confirmed;

                    await _blogService.AddWorkoutToBlogAsync(Blog.BlogID, oldWorkout);
                }
                catch
                {
                    Response.Redirect("/");
                }

            }
            else  //Nieuwe Workout
            {
                Blog = _service.GetBlogs().Where(b => b.CustomerID.Equals(SessionUser.ProfileID)).FirstOrDefault();

                await _blogService.AddWorkoutToBlogAsync(Blog.BlogID, Workout);
            }            
            
            return RedirectToPage("Index");
        }

        public IActionResult OnGetEdit(int? id)
        {
            OnGet();

            if (id == null)
            {
                return NotFound();
            }

            Workout = _service.GetWorkouts().FirstOrDefault(w => w.WorkoutID == id);

            SessionHelper.SetObjectAsJson(HttpContext.Session, "workout", Workout);

            if (Workout == null)
            {
                return NotFound();
            }
            return Page();
        }

    }
}
