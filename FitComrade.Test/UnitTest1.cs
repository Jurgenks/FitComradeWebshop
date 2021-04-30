using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Moq;
using FitComrade.Data.Entities;
using System.Collections.Generic;

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
        public void Can_Buy_Product()
        {
            //Arrange
            var M_product = new Mock<DbSet<Product>>();        
            var M_customer = new Mock<DbSet<Customer>>();
            var M_customerAdress = new Mock<DbSet<CustomerAdress>>();
            var M_order = new Mock<DbSet<Order>>();

            var mockContext = new Mock<TestContext>();
            mockContext.Setup(m => m.Products).Returns(M_product.Object);
            mockContext.Setup(m => m.Customers).Returns(M_customer.Object);
            mockContext.Setup(m => m.CustomerAdresses).Returns(M_customerAdress.Object);
            mockContext.Setup(m => m.Orders).Returns(M_order.Object);


            var mockSession = new MockHttpSession();

            Cart cart = new Cart();

            CustomerAdress customerAdress = new CustomerAdress { PostalCode = "2002MK", Adress = "Maaskantje" };

            Customer customer = new Customer
            {
                CustomerSurName = "Gerrie",
                CustomerName = "Van Boven",
                CustomerEmail = "test@test.com",
                CustomerPhone = "06xxxxxxxx",
                Payment = "IDEAL",
                Bank = "RaboBank",
            };

            //Act
            cart.Products = new List<Product>();
            cart.Products.Add(new Product { ProductName = "Pre Workout", ProductQuantity = 1 });

            //Assert
            var order = mockContext.Object.Orders;

            Assert.IsTrue(order != null);
        }
    }
}
