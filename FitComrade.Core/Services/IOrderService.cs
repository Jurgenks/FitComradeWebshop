using FitComrade.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitComrade.Core.Services
{
    public interface IOrderService
    {
        void RegisterCustomer(ISession session, Customer customer);
        void UpdateProfile(ISession session, Customer customer);
        void PlaceOrder(ISession session, Cart cart, CustomerAdress customerAdress, Payment payment);
        decimal GetSales(DateTime dateTime, string kind);
        void UpdateStatus(Order order);
        void RetourOrder(Order order);
    }
}
