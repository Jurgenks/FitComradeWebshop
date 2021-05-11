using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FitComrade.Models;
using Microsoft.EntityFrameworkCore;
using FitComrade.Domain.Entities;
using FitComrade.Data;
using FitComrade.Core;

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
            DataController dataController = new DataController(_context);

            Blog = dataController.CreateBlog(HttpContext.Session);

            return RedirectToPage("Edit");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            user = user.GetSession(HttpContext.Session, user);

            if(Workout.WorkoutID != 0 && user.ProfileID == 1)  //Bestaande Workout & admin
            {
                Blog = _context.Blogs.Where(b => b.Workouts.Contains(Workout)).FirstOrDefault();
            }
            else  //Nieuwe Workout
            {
                Blog = _context.Blogs.Where(b => b.CustomerID.Equals(user.ProfileID)).FirstOrDefault();
            }

            DataController dataController = new DataController(_context);

            await dataController.AddWorkoutToBlogAsync(Blog.BlogID, Workout);
            
            return Page();
        }
        
        public async Task<IActionResult> OnGetEditAsync(int? id)
        {
            OnGet();

            if (id == null)
            {
                return NotFound();
            }

            Workout = await _context.Workouts.FirstOrDefaultAsync(w => w.WorkoutID == id);

            if (Workout == null)
            {
                return NotFound();
            }
            return Page();
        }

    }
}
