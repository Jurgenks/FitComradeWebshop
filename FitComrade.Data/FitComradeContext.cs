using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FitComrade.Data.Entities;


namespace FitComrade.Data
{
    public class FitComradeContext : DbContext
    {
        public FitComradeContext()
        {
        }

        public FitComradeContext (DbContextOptions<FitComradeContext> options)
            : base(options)
        {
        }
                

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
             
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Workout> Workouts { get; set; }
        


    }
}
