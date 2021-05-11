using FitComrade.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitComrade.Test
{
    public class TestContext: DbContext
    {
        //Test Tables

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<CustomerAdress> CustomerAdresses { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        public virtual DbSet<Blog> Blogs { get; set; }

        public virtual DbSet<Workout> Workouts { get; set; }
    }
}
