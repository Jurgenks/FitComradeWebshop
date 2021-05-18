using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitComrade.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using FitComrade.Data;

namespace FitComrade.Core
{
    public class DataController : ControllerBase
    {
        private readonly FitComradeContext _context;

        public DataController(FitComradeContext context)
        {
            _context = context;
        }


        //Account
        public bool Login(ISession session, Customer profile) //Read Account
        {
            
            var login = _context.Customers.FirstOrDefault(s => s.UserName.Equals(profile.UserName) && s.Password.Equals(profile.Password));

            if (login != null)
            {

                //Je huidige sessie ontvangen variabelen om toegang te verkrijgen
                session.SetString("userName", profile.UserName);
                session.SetInt32("profileID", login.CustomerID);
                session.SetInt32("customerID", login.CustomerID);
                
                //Login succes
                return true;
            }
            //Login failed
            return false;
        }

        public bool Create(Customer profile) // Create Account
        {
            var register = _context.Customers.Where(s => s.UserName.Equals(profile.UserName));
            //Check of profile al bestaat
            if (register.Count() == 0)
            {                
                _context.Customers.Add(profile);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        //Webshop
        public void RegisterCustomer(ISession session, Customer customer , CustomerAdress customerAdress) //Create Customer
        {
            int customerID = 0;
            //Check of de customer account bestaat
            if (session.Keys.Contains("customerID"))
            {
                customerID = (int)session.GetInt32("customerID");
            }            
            if(customerID != 0)
            {
                var logged = _context.Customers.FirstOrDefault(c => c.CustomerID.Equals(customerID));
                customer.CustomerEmail = logged.CustomerEmail;
                customer.UserName = logged.UserName;
                return;
            }

            var register = _context.Customers.Where(s => s.CustomerEmail.Equals(customer.CustomerEmail));

            //Check of de customer bestaat
            if(register.Count() == 0) //customer bestaat niet
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();

                var newCustomer = _context.Customers.FirstOrDefault(s=>s.CustomerEmail.Equals(customer.CustomerEmail));
                newCustomer.Adresses = new List<CustomerAdress>();
                newCustomer.Adresses.Add(customerAdress);

                _context.Customers.Attach(newCustomer);
                _context.SaveChanges();

                session.SetInt32("customerID", newCustomer.CustomerID);

                var adress = _context.CustomerAdresses.FirstOrDefault(a => a.PostalCode.Equals(customerAdress.PostalCode));
                session.SetInt32("adressID", adress.CustomerAdressID);
            }
            else if(register.Count() > 0) //customer bestaat wel
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();

                var newCustomer = _context.Customers.FirstOrDefault(c => c.Equals(customer));
                newCustomer.Adresses = new List<CustomerAdress>();
                newCustomer.Adresses.Add(customerAdress);

                _context.Customers.Attach(newCustomer);
                _context.SaveChanges();

                session.SetInt32("customerID", newCustomer.CustomerID);

                var adress = _context.CustomerAdresses.FirstOrDefault(a => a.PostalCode.Equals(customerAdress.PostalCode));
                session.SetInt32("adressID", adress.CustomerAdressID);
                return;

            }
            
        }

        public void UpdateProfile(ISession session, Customer customer, CustomerAdress customerAdress) // Update Profile CustomerID
        {
            //Haal de profile op waar de gebruiker op is ingelogd
            var profile = _context.Customers.FirstOrDefault(s => s.CustomerID.Equals((int)session.GetInt32("customerID")));

            session.SetInt32("customerID", profile.CustomerID);

            profile.CustomerName = customer.CustomerName;

            profile.CustomerPhone = customer.CustomerPhone;

            profile.CustomerSurName = customer.CustomerSurName;

            profile.Bank = customer.Bank;

            profile.Payment = customer.Payment;

            var profileAdress = _context.CustomerAdresses.FirstOrDefault(a=>a.PostalCode.Equals(customerAdress.PostalCode));

            if(profileAdress == null || profileAdress.CustomerID != profile.CustomerID)
            {
                profile.Adresses = new List<CustomerAdress>();
                profile.Adresses.Add(customerAdress);
            }                   
            

            //Van het aangemaakte customer wordt het id ingesteld naar het profile en opgeslagen in de database
            //Update Profile
            _context.Customers.Attach(profile).State = EntityState.Modified;
            _context.SaveChanges();

            session.SetInt32("adressID", profileAdress.CustomerAdressID);
        }

        public void PlaceOrder(ISession session, Cart cart) //Create Order in Customer
        {
            int customerID = (int)session.GetInt32("customerID");
            int adressID = (int)session.GetInt32("adressID");
            //Haal customer op met customerID
            var customer = _context.Customers.FirstOrDefault(c => c.CustomerID.Equals(customerID));

            
            Order order = new Order();
            order.OrderDate = DateTime.Now;
            order.OrderStatus = "Paid";         //Alle bestellingen zijn direct "Paid" (fake)
            order.OrderPrice = cart.Total();
            order.CustomerAdressID = adressID;

            if (customer.Payment == "Credits")
            {
                var credits = _context.Credits.Where(item=>item.CustomerID.Equals(customer.CustomerID)).FirstOrDefault();
                if(credits.CreditValue < order.OrderPrice)
                {
                    return;
                }
                credits.CreditValue -= order.OrderPrice;
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
            var Orders = _context.Orders.ToList();
            var OrderDetails = _context.OrderDetails.ToList();
            decimal Sale = 0;
            switch(kind)
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
            if(order.OrderStatus == "Paid")
            {               
                //Order met de status "Confirmed" zijn omgeboekt
                order.OrderStatus = "Confirmed";
                //Update OrderStatus
                _context.Orders.Attach(order).State = EntityState.Modified;
                _context.SaveChanges();
            }
                    
        }

        public void AddProduct(Product product) //Create Product
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void RetourOrder(Order order)
        {
            if(order.OrderStatus == "Paid")
            {
                //Orders met de status "Dismissed" zijn afgewezen
                order.OrderStatus = "Dismissed";
                var credits = _context.Credits.Where(item=>item.CustomerID.Equals(order.CustomerID)).FirstOrDefault();
                
                //Update OrderStatus
                _context.Orders.Attach(order).State = EntityState.Modified;
                credits.CreditValue += order.OrderPrice;
                _context.SaveChanges();
            }
        }

        //Blog
        public Blog CreateBlog(ISession session) //Create Blog
        {            
            //Check of huidige sessie is ingelogd
            if (session.Keys.Contains("profileID"))
            {
                //Ontvang profile
                int profileID = (int)session.GetInt32("profileID");
                string userName = session.GetString("userName");

                Blog blog = new Blog();
                blog.BlogName = userName;
                blog.CustomerID = profileID;

                //Create Blog
                _context.Blogs.Add(blog);
                _context.SaveChanges();
                return blog;
            }
            return null;            
        } 

        public async Task AddWorkoutToBlogAsync(int id, Workout workout) //Create Workout in Blog
        {
            //Bestaat blog
            if (_context.Blogs.Where(blog => blog.BlogID.Equals(id)) != null)
            {
                var blog = _context.Blogs.FirstOrDefault(blog => blog.BlogID.Equals(id));

                var workouts = _context.Workouts.ToList();
                
                if(workout.WorkoutID == 0)  //Create workout
                { 
                    if(blog.Workouts == null)
                    {
                        blog.Workouts = new List<Workout>();
                    }
                    blog.Workouts.Add(workout);

                    //Update Blog
                    _context.Blogs.Attach(blog).State = EntityState.Modified;                    
                }
                else //Update workout
                {     
                    for(int i = 0;i<blog.Workouts.Count;i++)
                    {
                        if(blog.Workouts[i].WorkoutID == workout.WorkoutID)
                        {
                            blog.Workouts[i].Confirmed = workout.Confirmed;
                            blog.Workouts[i].Discription = workout.Discription;
                            blog.Workouts[i].WorkoutImage = workout.WorkoutImage;
                            blog.Workouts[i].WorkoutName = workout.WorkoutName;
                            blog.Workouts[i].WorkoutVideo = workout.WorkoutVideo;
                        }
                    }
                    _context.Blogs.Attach(blog).State = EntityState.Modified;                    
                }                
            }
            await _context.SaveChangesAsync();
        } 
        
    }
}
