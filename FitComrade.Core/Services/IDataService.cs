using FitComrade.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
