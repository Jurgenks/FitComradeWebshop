using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Moq;
using FitComrade.Domain.Entities;
using System.Collections.Generic;
using FitComrade.Core;
using Microsoft.AspNetCore.Http;

namespace FitComrade.Test
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void Can_Add_New_Product()
        {            
            //Arrange
            var mockSet = new Mock<DbSet<Product>>();

            var mockContext = new Mock<TestContext>();
            mockContext.Setup(m => m.Products).Returns(mockSet.Object);

            var service = new ProductService(mockContext.Object);

            //Act
            service.AddProduct("Tren",100,100);

            //Assert
            mockSet.Verify(m => m.Add(It.IsAny<Product>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void Can_Add_Workout_To_BlogAsync()
        {
            //Arrange            
            var mockSet = new Mock<DbSet<Blog>>();

            var mockContext = new Mock<TestContext>();
            mockContext.Setup(m => m.Blogs).Returns(mockSet.Object);

            var service = new BlogService(mockContext.Object);
            //Act
            service.AddBlog(2, "Fietsen", "Fiets");
            //Assert
            mockSet.Verify(m => m.Add(It.IsAny<Blog>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        

        [TestMethod]
        public void Can_Place_Order()
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
                CustomerPhone = "x",
                Bank ="x",
                Payment ="x"
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
                DataController dataController = new DataController(context);
                dataController.ControllerContext.HttpContext = mockHttpContext.Object;                
                dataController.RegisterCustomer(session, customer, customerAdress);
                
                if((int)session.GetInt32("customerID") != 0)
                {
                    dataController.PlaceOrder(session, cart);
                }

                var orders = context.Orders;
                int count = 0;
                foreach (var item in orders)
                {
                    count++;
                }
                Assert.AreEqual(1, count);
            }
            
        }
    }
}
