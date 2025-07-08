using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;
using Ambev.DeveloperEvaluation.WebApi.Features.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Controller
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<ILogger<ProductsController>> _loggerMock;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _loggerMock = new Mock<ILogger<ProductsController>>();
            _controller = new ProductsController(_productServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult()
        {
            var id = 1;
            var product = new Product { Id = id, Name = "tests" };

            _productServiceMock.Setup(p => p.GetById(id)).ReturnsAsync(product);

            var result = await _controller.GetById(id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(id, returnedProduct.Id);
        }

        [Fact]
        public async Task Create_ReturnsOkResult()
        {
            var dto = new ProductDTO { Id = 1, Name = "tests" };
            var product = new Product { Id = 1, Name = "tests" };

            _productServiceMock.Setup(p => p.Create(dto)).ReturnsAsync(product);

            var result = await _controller.Create(dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<Product>(okResult.Value);
            Assert.Equal("tests", returned.Name);
        }

        [Fact]
        public async Task Update_ReturnsOk()
        {
            var dto = new ProductDTO { Id = 1, Name = "Teste" };
            _productServiceMock.Setup(p => p.Update(dto)).Returns(Task.CompletedTask);

            var result = await _controller.Update(dto);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsOk()
        {
            var id = 1;
            _productServiceMock.Setup(p => p.Delete(id)).Returns(Task.CompletedTask);

            var result = await _controller.Delete(id);

            Assert.IsType<OkResult>(result);
        }
    }
}
