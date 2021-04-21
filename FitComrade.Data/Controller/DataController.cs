using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitComrade.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FitComrade.Data
{
    public class DataController : ControllerBase
    {
        private readonly FitComradeContext _context;

        public DataController(FitComradeContext context)
        {
            _context = context;
        }

        //Account
        public bool Login(ISession session, Profile profile) //Read Account
        {
            
            var login = _context.Profiles.Where(s => s.UserName.Equals(profile.UserName) && s.Password.Equals(profile.Password)).FirstOrDefault();

            if (login != null)
            {

                //Je huidige sessie ontvangen variabelen om toegang te verkrijgen
                session.SetString("userName", profile.UserName);
                session.SetInt32("profileID", login.ProfileID);

                //Heeft profile een CustomerID, ontvangt je huidige sessie het CustomerID van het Profiel
                if(login.CustomerID != 0)
                {
                    session.SetInt32("customerID", login.CustomerID);
                }
                //Login succes
                return true;
            }
            //Login failed
            return false;
        }

        public bool Create(Profile profile) // Create Account
        {
            var register = _context.Profiles.Where(s => s.UserName.Equals(profile.UserName));
            //Check of profile al bestaat
            if (register.Count() == 0)
            {
                _context.Profiles.Add(profile);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        //Webshop
        public void RegisterCustomer(ISession session, Customer customer) //Create Customer
        {
            var register = _context.Customers.Where(s => s.CustomerEmail.Equals(customer.CustomerEmail));
            //Check of de customer bestaat
            if(register.Count() == 0) //customer bestaat niet
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                var id = _context.Customers.Where(s=>s.CustomerEmail.Equals(customer.CustomerEmail)).FirstOrDefault();
                session.SetInt32("customerID", id.CustomerID);
            }
            else if(register.Count() > 0) //customer bestaat wel
            {
                //id wordt een List van alle customers met dezelfde email
                var id = register.ToList();
                for(int i = 0; i < register.Count();i++)
                {
                    if (customer.CustomerPostalCode == id[i].CustomerPostalCode) //Same Email, Same PostalCode, old customer
                    {                        
                        session.SetInt32("customerID", id[i].CustomerID);
                        return;
                    }
                    else if (!id.Contains(customer)) // Same Email, Different fields, new customer
                    {
                        _context.Customers.Add(customer);
                        _context.SaveChanges();
                        var newid = _context.Customers.Where(s => s.CustomerPostalCode.Equals(customer.CustomerPostalCode)).FirstOrDefault();
                        session.SetInt32("customerID", newid.CustomerID);
                        return;
                    }
                }                
                
            }
            
        }

        public void UpdateProfile(ISession session, int id) // Update Profile CustomerID
        {
            //Haal het profile op waar de gebruiker op is ingelogd
            var profile = _context.Profiles.Where(s => s.ProfileID.Equals((int)session.GetInt32("profileID"))).FirstOrDefault();
            
            profile.CustomerID = id;
            //Van het aangemaakte customer wordt het id ingesteld naar het profile en opgeslagen in de database
            //Update Profile
            _context.Profiles.Attach(profile).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void PlaceOrder(int customerID, Cart cart) //Create Order in Customer
        {
            //Haal customer op met customerID
            var customer = _context.Customers.Where(c => c.CustomerID.Equals(customerID)).FirstOrDefault();

            Order order = new Order();
            order.OrderDate = DateTime.Now;
            order.OrderStatus = "Paid";         //Alle bestellingen zijn direct "Paid" (fake)
            order.OrderPrice = cart.Total();

            if (order.OrderStatus == "Paid")
            {              
                List<OrderDetail> orderDetails = new List<OrderDetail>();
                foreach (var item in cart.Products.Where(item => item != null)) //Create OrderDetail
                {                    
                    orderDetails.Add(new OrderDetail
                    {                        
                        ProductID = item.ProductID,
                        ProductName = item.ProductName,
                        ProductPrice = item.ProductPrice,
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

        public void UpdateStock(Order order) //Update Order & Products
        {
            //Orders met de status "Paid" moeten hun producten nog afschrijven van de voorraad
            if(order.OrderStatus == "Paid")
            {
                //Haal bijbehorende OrderDetails op
                var OrderDetails = _context.OrderDetails.Where(s => s.OrderID.Equals(order.OrderID));
                //Haal de Producten van de voorraad op
                var Products = _context.Products.ToList();
                
                foreach (var item in OrderDetails)
                {
                    //Haal het product op dat gelijk is aan het product in OrderDetail
                    var Product = Products.Where(p => p.ProductID.Equals(item.ProductID)).ToList();
                    //Bereken nieuwe voorraad van het product
                    for (int i = 0; i < Product.Count(); i++)
                    {
                        Product[i].ProductQuantity -= item.Quantity;
                        _context.Products.Attach(Product[i]).State = EntityState.Modified;
                    }
                }
                //Order met de status "Confirmed" zijn omgeboekt
                order.OrderStatus = "Confirmed";
                //Update OrderStatus
                _context.Orders.Attach(order).State = EntityState.Modified;
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
                blog.ProfileID = profileID;

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
                var blog = _context.Blogs.Where(blog => blog.BlogID.Equals(id)).FirstOrDefault();

                var workouts = _context.Workouts.ToList();
                
                if(workout.WorkoutID == 0)  //Create workout
                {                    
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
