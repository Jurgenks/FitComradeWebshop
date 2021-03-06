using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Moq;
using FitComrade.Domain.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using FitComrade.Helpers;
using FitComrade.Core.Services;

namespace FitComrade.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TC01_Can_Add_Product()
        {
            //Arrange
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession session = new MockHttpSession();
            mockHttpContext.Setup(s => s.Session).Returns(session);

            var options = new DbContextOptionsBuilder<Data.FitComradeContext>()
                .UseInMemoryDatabase(databaseName: "FitComradeContextDB")
                .Options;

            Product product = new Product
            {
                ProductName = "Tren",
                ProductPrice = 100,
                ProductQuantity = 100
            };
            //Act
            using (var context = new Data.FitComradeContext(options))
            {
                ProductService productController = new ProductService(context);
                productController.AddProduct(product);

                var Products = context.Products;

                //Assert
                Assert.IsTrue(Products != null);
            }
        }

        [TestMethod]
        public async Task TC02_Can_Add_Workout_To_BlogAsync()
        {
            //Arrange
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession session = new MockHttpSession();
            mockHttpContext.Setup(s => s.Session).Returns(session);

            var options = new DbContextOptionsBuilder<Data.FitComradeContext>()
                .UseInMemoryDatabase(databaseName: "FitComradeContextDB")
                .Options;
            Customer customer = new Customer
            {
                UserName = "test",
                Password = "test",
                CustomerEmail = "test@test.test"
            };

            Workout workout = new Workout
            {
                WorkoutName = "Fietsen",
                Discription = "Om vooruit te gaan op de fiets moet je op de trappers duwen moet je voeten."
            };
            //Act
            session.SetInt32("profileID", 2);
            session.SetString("userName", "Barrie");

            using (var context = new Data.FitComradeContext(options))
            {
                AccountService accountService = new AccountService(context);
                accountService.CreateProfile(customer);
                accountService.LoginProfile(session, customer);

                BlogService blogService = new BlogService(context);
                blogService.CreateBlog(session);
                await blogService.AddWorkoutToBlogAsync(1, workout);

                var Workout = context.Workouts.First();
                var Blog = context.Blogs.Where(w => w.Workouts.Contains(Workout));

                Assert.IsTrue(Blog != null);
            }
        }

        [TestMethod]
        public async Task TC03_Can_Confirm_Workout_From_BlogAsync()
        {
            //Arrange
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession session = new MockHttpSession();
            mockHttpContext.Setup(s => s.Session).Returns(session);

            var options = new DbContextOptionsBuilder<Data.FitComradeContext>()
                .UseInMemoryDatabase(databaseName: "FitComradeContextDB")
                .Options;

            Customer customer = new Customer
            {
                UserName = "test",
                Password = "test",
                CustomerEmail = "test@test.test"
            };

            Workout workout = new Workout
            {
                WorkoutName = "Fietsen",
                Discription = "Om vooruit te gaan op de fiets moet je op de trappers duwen moet je voeten."
            };
            //Act
            session.SetInt32("profileID", 2);
            session.SetString("userName", "Barrie");

            using (var context = new Data.FitComradeContext(options))
            {
                AccountService accountService = new AccountService(context);
                accountService.CreateProfile(customer);
                accountService.LoginProfile(session, customer);

                BlogService blogService = new BlogService(context);
                blogService.CreateBlog(session);
                await blogService.AddWorkoutToBlogAsync(1, workout);

                var Workout = context.Workouts.First();
                var Blog = context.Blogs.Where(w => w.Workouts.Contains(Workout));

                session.SetInt32("profileID", 1);
                Workout.Confirmed = true;
                await blogService.AddWorkoutToBlogAsync(1, Workout);

                Workout = context.Workouts.First();
                Assert.IsTrue(Workout.Confirmed);
            }
        }

        [TestMethod]
        public void TC04_Can_Create_New_Cart()
        {
            //Arrange
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession session = new MockHttpSession();
            mockHttpContext.Setup(s => s.Session).Returns(session);

            var options = new DbContextOptionsBuilder<Data.FitComradeContext>()
                .UseInMemoryDatabase(databaseName: "FitComradeContextDB")
                .Options;

            Product product = new Product
            {
                ProductName = "Pre Workout",
                ProductPrice = 25,
                ProductQuantity = 100
            };
            List<Product> cart = new List<Product>();
            //Act          

            using (var context = new Data.FitComradeContext(options))
            {
                context.Products.Add(product);
                context.SaveChanges();

                CartService cartService = new CartService();

                var dbProduct = context.Products.FirstOrDefault(item => item.ProductID.Equals(1));
                cart = cartService.NewCart(dbProduct);

                SessionHelper.SetObjectAsJson(session, "cart", cart);
                cart = SessionHelper.GetObjectFromJson<List<Product>>(session, "cart");
                //Assert
                Assert.IsTrue(cart != null);
                Assert.IsTrue(cart.Count == 1);
            }
        }

        [TestMethod]
        public void TC05_Can_Place_Order()
        {
            //Arrange
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
            MockHttpSession session = new MockHttpSession();
            mockHttpContext.Setup(s => s.Session).Returns(session);


            Customer customer = new Customer
            {
                CustomerName = "Van Boven",
                CustomerEmail = "Gerrie.Boven@hotmail.com",
                CustomerSurName = "Gerrie",
                CustomerPhone = "x"
            };
            Payment payment1 = new Payment
            {
                PaymentMethod = "IDEAL"
            };
            Payment payment2 = new Payment
            {
                PaymentMethod = "Credits"
            };
            CustomerAdress customerAdress = new CustomerAdress
            {
                PostalCode = "5023CE"
            };

            Product product = new Product
            {
                ProductID = 1,
                ProductName = "Tren",
                ProductPrice = 100,
                ProductQuantity = 1
            };

            Cart cart = new Cart();

            var options = new DbContextOptionsBuilder<Data.FitComradeContext>()
                .UseInMemoryDatabase(databaseName: "FitComradeContextDB")
                .Options;
            //Act
            cart.Products = new List<Product>();
            cart.Products.Add(product);

            using (var context = new Data.FitComradeContext(options))
            {
                context.Payments.Add(payment1); //Vaste betalingsopties
                context.Payments.Add(payment2);

                OrderService orderService = new OrderService(context);
                orderService.RegisterCustomer(session, customer);

                if ((int)session.GetInt32("customerID") != 0)
                {
                    orderService.PlaceOrder(session, cart, customerAdress, payment1);
                }

                var orders = context.Orders;

                //Assert

                Assert.IsTrue(orders != null);
            }

        }
    }
}
