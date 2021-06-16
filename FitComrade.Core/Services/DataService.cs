using FitComrade.Data;
using FitComrade.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FitComrade.Core.Services
{
    public class DataService : IDataService
    {
        private readonly FitComradeContext _context;

        public DataService(FitComradeContext context)
        {
            _context = context;
        }

        public List<Product> GetProducts()
        {
            var products = _context.Products.ToList();

            if (products != null)
            {
                return products;
            }
            else
            {
                return null;
            }
        }

        public List<Payment> GetPayments()
        {
            var payments = _context.Payments.ToList();

            if (payments != null)
            {
                return payments;
            }
            else
            {
                return null;
            }
        }

        public List<Customer> GetCustomers()
        {
            var customers = _context.Customers.ToList();

            if (customers != null)
            {
                return customers;
            }
            else
            {
                return null;
            }
        }

        public List<CustomerAdress> GetCustomerAdresses()
        {
            var customerAdresses = _context.CustomerAdresses.ToList();

            if (customerAdresses != null)
            {
                return customerAdresses;
            }
            else
            {
                return null;
            }
        }

        public List<Blog> GetBlogs()
        {
            var blogs = _context.Blogs.ToList();

            if(blogs != null)
            {
                return blogs;
            }
            else
            {
                return null;
            }
        }

        public List<Workout> GetWorkouts()
        {
            var workouts = _context.Workouts.ToList();

            if (workouts != null)
            {
                return workouts;
            }
            else
            {
                return null;
            }
        }

        public List<Credit> GetCredits()
        {
            var credits = _context.Credits.ToList();

            if (credits != null)
            {
                return credits;
            }
            else
            {
                return null;
            }
        }

        public List<CreditCode> GetCreditCodes()
        {
            var creditCodes = _context.CreditCodes.ToList();

            if (creditCodes != null)
            {
                return creditCodes;
            }
            else
            {
                return null;
            }
        }

        public List<Order> GetOrders()
        {
            var orders = _context.Orders.ToList();

            if(orders != null)
            {
                return orders;
            }
            else
            {
                return null;
            }
        }

        public List<OrderDetail> GetOrderDetails()
        {
            var orderDetails = _context.OrderDetails.ToList();

            if (orderDetails != null)
            {
                return orderDetails;
            }
            else
            {
                return null;
            }
        }

    }
}
