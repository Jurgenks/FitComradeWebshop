using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Moq;
using FitComrade.Data.Entities;
using FitComrade.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            mockContext.Setup(m => m.TestProducts).Returns(mockSet.Object);

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
            mockContext.Setup(m => m.TestBlogs).Returns(mockSet.Object);

            var service = new BlogService(mockContext.Object);
            //Act
            service.AddBlog(2, "Fietsen", "Fiets");
            //Assert
            mockSet.Verify(m => m.Add(It.IsAny<Blog>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
