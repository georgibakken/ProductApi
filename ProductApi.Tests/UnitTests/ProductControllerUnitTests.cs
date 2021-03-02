using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Xunit.Abstractions;
using Moq;
using ProductApi.Models;
using ProductApi.Controllers;
using System.Linq;

namespace ProductApi.Tests
{
    public class ProductControllerUnitTests
    {
        [Fact]
        public async Task GetAllProducts()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetAllProductsAsync())
                .ReturnsAsync(GetTestProducts());
            var controller = new ProductsController(mockRepo.Object);

            // Act
            var result = await controller.GetProducts();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<ProductDTO>>>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<ProductDTO>>(actionResult.Value);
            var product = returnValue.FirstOrDefault();
            Assert.Equal("TV", product.Name);
        }

        [Fact]
        public async Task GetNonExistentProduct()
        {
            // Arrange
            long nonExistentProductId = 123;
            var mockRepo = new Mock<IProductRepository>();
            var controller = new ProductsController(mockRepo.Object);

            // Act
            var result = await controller.GetProduct(nonExistentProductId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProductDTO>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetExistentProduct()
        {
            // Arrange
            long testProductId = 2;
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testProductId))
                .ReturnsAsync(GetTestProducts().Find(
                    p => p.Id == testProductId));
            var controller = new ProductsController(mockRepo.Object);

            // Act
            var result = await controller.GetProduct(testProductId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProductDTO>>(result);
            var returnValue = Assert.IsAssignableFrom<ProductDTO>(actionResult.Value);
            Assert.Equal("Computer", returnValue.Name);


        }

        [Fact]
        public async Task CreatenNewValidProduct()
        {
            // Arrange
            long testProductId = 123;
            string testName = "Banana";
            double testPrice = 11.25;
            var mockRepo = new Mock<IProductRepository>();
            var controller = new ProductsController(mockRepo.Object);

            // Act
            var result = await controller.CreateProduct(new ProductDTO()
            {
                Id = testProductId,
                Name = testName,
                Price = testPrice
            });

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProductDTO>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnValue = Assert.IsAssignableFrom<ProductDTO>(createdAtActionResult.Value);
            Assert.Equal(testName, returnValue.Name);
            Assert.Equal(testPrice, returnValue.Price);
        }

        private List<Product> GetTestProducts()
        {
            var products = new List<Product>();
            products.Add(new Product()
            {
                Id = 1,
                Name = "TV",
                Price = 250.25
            });
            products.Add(new Product()
            {
                Id = 2,
                Name = "Computer",
                Price = 175
            });
            return products;
        }
    }
}