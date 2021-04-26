using FitComrade.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitComrade.Test
{
    public class TestContext: DbContext
    {
        //Test DbSets

        public virtual DbSet<Product> TestProducts { get; set; }

        public virtual DbSet<Blog> TestBlogs { get; set; }

        public virtual DbSet<Workout> TestWorkouts { get; set; }
    }
}
