using FitComrade.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FitComrade.Core.Services
{
    public interface IDataService
    {
        List<Product> GetProducts();
        List<Payment> GetPayments();
        List<Customer> GetCustomers();
        List<CustomerAdress> GetCustomerAdresses();
        List<Blog> GetBlogs();
        List<Workout> GetWorkouts();
        List<Credit> GetCredits();
        List<CreditCode> GetCreditCodes();
        List<Order> GetOrders();
        List<OrderDetail> GetOrderDetails();
        Task<List<Product>> GetProductsAsync();
        Task<List<Payment>> GetPaymentsAsync();
        Task<List<Customer>> GetCustomersAsync();
        Task<List<CustomerAdress>> GetCustomerAdressesAsync();
        Task<List<Blog>> GetBlogsAsync(int id);
        Task<List<Workout>> GetWorkoutsAsync(bool b);
        Task<List<Credit>> GetCreditsAsync();
        Task<List<CreditCode>> GetCreditCodesAsync();
        Task<List<Order>> GetOrdersAsync();
        Task<List<OrderDetail>> GetOrderDetailsAsync();
    }
}
