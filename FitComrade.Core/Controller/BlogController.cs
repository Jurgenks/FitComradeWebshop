using FitComrade.Data;
using FitComrade.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitComrade.Core.Controller
{
    public class BlogController : ControllerBase
    {
        private readonly FitComradeContext _context;

        public BlogController(FitComradeContext context)
        {
            _context = context;
        }

        //Blog
        public Blog CreateBlog(ISession session) //Create Blog
        {
            //Check of huidige sessie is ingelogd
            if (session.Keys.Contains("profileID"))
            {
                //Ontvang profile
                int profileID = (int)session.GetInt32("profileID");
                string userName = session.GetString("userName");
                var profile = _context.Customers.FirstOrDefault(item => item.CustomerID.Equals(profileID));

                profile.Blog = new Blog
                {
                    BlogName = userName,
                    CustomerID = profileID
                };

                //Create Blog
                _context.Customers.Attach(profile).State = EntityState.Modified;
                _context.SaveChanges();
                return profile.Blog;
            }
            return null;
        }

        public async Task AddWorkoutToBlogAsync(int id, Workout workout) //Create Workout in Blog
        {
            //Bestaat blog
            if (_context.Blogs.Where(blog => blog.BlogID.Equals(id)) != null)
            {
                var blog = _context.Blogs.FirstOrDefault(blog => blog.BlogID.Equals(id));

                var workouts = _context.Workouts.ToList();

                if (workout.WorkoutID == 0)  //Create workout
                {
                    if (blog.Workouts == null)
                    {
                        blog.Workouts = new List<Workout>();
                    }
                    blog.Workouts.Add(workout);

                    //Update Blog
                    _context.Blogs.Attach(blog).State = EntityState.Modified;
                }
                else //Update workout
                {
                    for (int i = 0; i < blog.Workouts.Count; i++)
                    {
                        if (blog.Workouts[i].WorkoutID == workout.WorkoutID)
                        {
                            blog.Workouts[i].Confirmed = workout.Confirmed;
                            blog.Workouts[i].Discription = workout.Discription;
                            blog.Workouts[i].WorkoutImage = workout.WorkoutImage;
                            blog.Workouts[i].WorkoutName = workout.WorkoutName;
                            blog.Workouts[i].WorkoutVideo = workout.WorkoutVideo;
                        }
                    }
                    _context.Blogs.Attach(blog).State = EntityState.Modified;
                }
            }
            await _context.SaveChangesAsync();
        }

        
    }
}
