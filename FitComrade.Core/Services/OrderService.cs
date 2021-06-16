using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitComrade.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using FitComrade.Data;

namespace FitComrade.Core.Services
{
    public class OrderService
    {
        private readonly FitComradeContext _context;

        public OrderService(FitComradeContext context)
        {
            _context = context;
        }


        //Webshop
        public void RegisterCustomer(ISession session, Customer customer) //Create Customer
        {
            int customerID = 0;
            //Check of de customer account bestaat
            if (session.Keys.Contains("customerID"))
            {
                customerID = (int)session.GetInt32("customerID");
            }
            if (customerID != 0)
            {
                var logged = _context.Customers.FirstOrDefault(c => c.CustomerID.Equals(customerID));
                customer.CustomerEmail = logged.CustomerEmail;
                customer.UserName = logged.UserName;
                return;
            }

            var register = _context.Customers.Where(s => s.CustomerEmail.Equals(customer.CustomerEmail));

            //Check of de customer bestaat
            if (register.Count() == 0) //customer bestaat niet
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();

                var newCustomer = _context.Customers.FirstOrDefault(s => s.CustomerEmail.Equals(customer.CustomerEmail));

                session.SetInt32("customerID", newCustomer.CustomerID);
            }
            else if (register.Count() > 0) //customer bestaat wel
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();

                var newCustomer = _context.Customers.FirstOrDefault(c => c.Equals(customer));

                _context.Customers.Attach(newCustomer);
                _context.SaveChanges();

                session.SetInt32("customerID", newCustomer.CustomerID);

                return;

            }

        }

        public void UpdateProfile(ISession session, Customer customer) // Update Profile CustomerID
        {
            //Haal de profile op waar de gebruiker op is ingelogd
            var profile = _context.Customers.FirstOrDefault(s => s.CustomerID.Equals((int)session.GetInt32("customerID")));

            session.SetInt32("customerID", profile.CustomerID);

            profile.CustomerName = customer.CustomerName;

            profile.CustomerPhone = customer.CustomerPhone;

            profile.CustomerSurName = customer.CustomerSurName;

            //Van het aangemaakte customer wordt het id ingesteld naar het profile en opgeslagen in de database
            //Update Profile
            _context.Customers.Attach(profile).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void PlaceOrder(ISession session, Cart cart, CustomerAdress customerAdress, Payment payment) //Create Order in Customer
        {
            int customerID = (int)session.GetInt32("customerID");
            //Haal customer op met customerID
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerID.Equals(customerID));
            var paymentMethods = _context.Payments;

            Order order = new Order();
            order.OrderDate = DateTime.Now;
            order.OrderPrice = cart.Total();
            order.CustomerAdress = customerAdress;

            if (payment.PaymentMethod == paymentMethods.FirstOrDefault(item => item.PaymentMethod.Equals("Credits")).PaymentMethod)
            {
                var credits = _context.Credits.Where(item => item.CustomerID.Equals(customer.CustomerID)).FirstOrDefault();
                if (credits.CreditValue < order.OrderPrice)
                {
                    return;
                }
                credits.CreditValue -= order.OrderPrice;
                order.OrderStatus = "Paid";
                order.PaymentID = paymentMethods.FirstOrDefault(item => item.PaymentMethod.Equals("Credits")).PaymentID;
            }

            if (payment.PaymentMethod == paymentMethods.FirstOrDefault(item => item.PaymentMethod.Equals("IDEAL")).PaymentMethod)
            {
                order.OrderStatus = "Paid"; //fake
                order.PaymentID = paymentMethods.FirstOrDefault(item => item.PaymentMethod.Equals("IDEAL")).PaymentID;
            }

            if (order.OrderStatus == "Paid")
            {
                List<OrderDetail> orderDetails = new List<OrderDetail>();
                foreach (var item in cart.Products.Where(item => item != null)) //Create OrderDetail
                {
                    orderDetails.Add(new OrderDetail
                    {
                        ProductID = item.ProductID,
                        Quantity = item.ProductQuantity,
                        TotalPrice = item.ProductPrice * item.ProductQuantity
                    });

                }
                order.OrderDetails = orderDetails;
                customer.Orders = new List<Order>();    //fixed bug customer.Orders == NULL

                customer.Orders.Add(order);

                //Update Customers                
                _context.Customers.Attach(customer).State = EntityState.Modified;
                _context.SaveChanges();
            }

        }

        public decimal GetSales(DateTime dateTime, string kind) //Read Sales from OrderDetail
        {
            var Orders = _context.Orders.Where(o => o.OrderStatus != "Dismissed").ToList();
            var OrderDetails = _context.OrderDetails.Where(o => o.Order.OrderStatus != "Dismissed").ToList();
            decimal Sale = 0;
            switch (kind)
            {
                case "Month":
                    //Haal alle orders op van de Maand
                    var OrderMonth = Orders.Where(m => m.OrderDate.Month.Equals(dateTime.Month)).ToList();
                    if (OrderMonth != null)
                    {
                        foreach (var item in OrderDetails)
                        {
                            for (int i = 0; i < OrderMonth.Count(); i++)
                            {
                                if (item.OrderID == OrderMonth[i].OrderID)
                                {
                                    Sale += item.TotalPrice;
                                }
                            }
                        }
                    }
                    return Sale;
                case "Day":
                    //Haal alle orders op van vandaag
                    var OrderDay = Orders.Where(m => m.OrderDate.Day.Equals(dateTime.Day)).ToList();
                    if (OrderDay != null)
                    {
                        foreach (var item in OrderDetails)
                        {
                            for (int i = 0; i < OrderDay.Count(); i++)
                            {
                                if (item.OrderID == OrderDay[i].OrderID)
                                {
                                    Sale += item.TotalPrice;
                                }
                            }
                        }
                    }
                    return Sale;
                case "Year":
                    //Haal alle orders op van dit jaar
                    var OrderYear = Orders.Where(m => m.OrderDate.Year.Equals(dateTime.Year)).ToList();
                    if (OrderYear != null)
                    {
                        foreach (var item in OrderDetails)
                        {
                            for (int i = 0; i < OrderYear.Count(); i++)
                            {
                                if (item.OrderID == OrderYear[i].OrderID)
                                {
                                    Sale += item.TotalPrice;
                                }
                            }
                        }
                    }
                    return Sale;
            }
            return Sale;
        }

        public void UpdateStatus(Order order) //Update Order & Products
        {
            //Orders met de status "Paid" moeten hun producten nog afschrijven van de voorraad
            if (order.OrderStatus == "Paid")
            {
                //Order met de status "Confirmed" zijn omgeboekt
                order.OrderStatus = "Confirmed";
                //Update OrderStatus
                _context.Orders.Attach(order).State = EntityState.Modified;
                _context.SaveChanges();
            }

        }

        public void RetourOrder(Order order)
        {
            if (order.OrderStatus == "Paid")
            {
                //Orders met de status "Dismissed" zijn afgewezen
                order.OrderStatus = "Dismissed";
                var credits = _context.Credits.Where(item => item.CustomerID.Equals(order.CustomerID)).FirstOrDefault();

                //Update OrderStatus
                _context.Orders.Attach(order).State = EntityState.Modified;
                credits.CreditValue += order.OrderPrice;
                _context.SaveChanges();
            }
        }



    }
}
