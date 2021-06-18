using FitComrade.Data;
using FitComrade.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            return products;

        }

        public List<Payment> GetPayments()
        {
            var payments = _context.Payments.ToList();

            return payments;
        }

        public List<Customer> GetCustomers()
        {
            var customers = _context.Customers.ToList();

            return customers;
        }

        public List<CustomerAdress> GetCustomerAdresses()
        {
            var customerAdresses = _context.CustomerAdresses.ToList();

            return customerAdresses;
        }

        public List<Blog> GetBlogs()
        {
            var blogs = _context.Blogs.ToList();

            return blogs;
        }

        public List<Workout> GetWorkouts()
        {
            var workouts = _context.Workouts.ToList();

            return workouts;
        }

        public List<Credit> GetCredits()
        {
            var credits = _context.Credits.ToList();

            return credits;
        }

        public List<CreditCode> GetCreditCodes()
        {
            var creditCodes = _context.CreditCodes.ToList();

            return creditCodes;
        }

        public List<Order> GetOrders()
        {
            var orders = _context.Orders.ToList();

            return orders;

        }

        public List<OrderDetail> GetOrderDetails()
        {
            var orderDetails = _context.OrderDetails.ToList();

            return orderDetails;

        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var products = await _context.Products.ToListAsync();

            return products;

        }

        public async Task<List<Payment>> GetPaymentsAsync()
        {
            var payments = await _context.Payments.ToListAsync();

            return payments;
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            return customers;
        }

        public async Task<List<CustomerAdress>> GetCustomerAdressesAsync()
        {
            var customerAdresses = await _context.CustomerAdresses.ToListAsync();
            return customerAdresses;
        }

        public async Task<List<Blog>> GetBlogsAsync(int id)
        {
            var blogs = await _context.Blogs.ToListAsync();

            if(id > 0)
            {
                blogs = await _context.Blogs.Where(b => b.CustomerID.Equals(id)).ToListAsync();
            }

            return blogs;
        }

        public async Task<List<Workout>> GetWorkoutsAsync(bool b)
        {
            var workouts = await _context.Workouts.Where(w=>w.Confirmed.Equals(b)).ToListAsync();

            return workouts;
        }

        public async Task<List<Credit>> GetCreditsAsync()
        {
            var credits = await _context.Credits.ToListAsync();
            return credits;
        }

        public async Task<List<CreditCode>> GetCreditCodesAsync()
        {
            var creditCodes = await _context.CreditCodes.ToListAsync();
            return creditCodes;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            return orders;
        }

        public async Task<List<OrderDetail>> GetOrderDetailsAsync()
        {
            var orderDetails = await _context.OrderDetails.ToListAsync();
            return orderDetails;
        }

    }
}
