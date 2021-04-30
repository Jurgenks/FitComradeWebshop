using System;
using System.Collections.Generic;
using System.Text;
using FitComrade.Data;
using FitComrade.Data.Entities;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FitComrade.Test
{
    public class BlogService
    {
        private TestContext _context;

        public BlogService(TestContext context)
        {
            _context = context;
        }

        public void AddBlog(int customerID, string workoutName, string discription)
        {
            List<Workout> workouts = new List<Workout>();

            workouts.Add(new Workout {WorkoutName = workoutName, Discription = discription});

            _context.Blogs.Add(new Blog {CustomerID = customerID, Workouts = workouts});

            _context.SaveChanges();
        }

        

        public List<Blog> GetAllBlogs()
        {
            var query = from b in _context.Blogs
                        orderby b.BlogName
                        select b;

            return query.ToList();
        }
        
        public async Task<List<Blog>> GetAllBlogsAsync()
        {
            var query = from b in _context.Blogs
                        orderby b.BlogName
                        select b;

            return await query.ToListAsync();
        }
    }
}
