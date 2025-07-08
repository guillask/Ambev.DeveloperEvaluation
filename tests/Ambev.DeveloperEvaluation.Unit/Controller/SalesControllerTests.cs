using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Models;
using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Controller
{
    public class SalesControllerTests
    {
        private readonly SalesController _controller;
        private readonly Mock<ISaleService> _serviceMock;
        private readonly Mock<ILogger<SalesController>> _loggerMock;

        // Objetos reutilizáveis
        private readonly Sale _saleDomain;
        private readonly SaleDTO _saleDTO;
        private readonly List<int> _productIds;

        public SalesControllerTests()
        {
            _serviceMock = new Mock<ISaleService>();
            _loggerMock = new Mock<ILogger<SalesController>>();
            _controller = new SalesController(_serviceMock.Object, _loggerMock.Object);

            _saleDomain = new Sale
            {
                Id = 1,
                Customer = "Test",
                Branch = "Teste",
                ProductsDiscount = new List<ProductsDiscountModel>()
            };

            _saleDTO = new SaleDTO
            {
                Id = 1,
                Customer = "Test",
                Branch = "Teste",
                ProductsDiscount = new List<ProductsDiscountModel>()
            };

            _productIds = new List<int> { 1, 2, 3 };
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenSaleExists()
        {
            _serviceMock.Setup(s => s.GetById(_saleDomain.Id)).ReturnsAsync(_saleDomain);

            var result = await _controller.GetById(_saleDomain.Id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(_saleDomain, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsBadRequest_OnException()
        {
            var saleId = 999;
            _serviceMock.Setup(s => s.GetById(saleId)).ThrowsAsync(new Exception("Not found"));

            var result = await _controller.GetById(saleId);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Not found", badRequest.Value);
        }

        [Fact]
        public async Task Create_ReturnsOk_WhenSaleIsCreated()
        {
            _serviceMock.Setup(s => s.Create(_saleDTO)).ReturnsAsync(_saleDomain);

            var result = await _controller.Create(_saleDTO);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(_saleDomain, ok.Value);
        }

        [Fact]
        public async Task Filter_ReturnsOk_WhenSuccess()
        {
            var salesList = new List<Sale>
            {
                new Sale { Id = 1, Customer = "Test", Branch = "Teste", ProductsDiscount = new() }
            };

            _serviceMock.Setup(s => s.Filter(null, "Test", null)).ReturnsAsync(salesList);

            var result = await _controller.Filter(null, "Test", null);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(salesList, ok.Value);
        }


        [Fact]
        public async Task AddSale_ReturnsOk_WhenSuccess()
        {           
            var productIds = new List<int> { 1, 2 };
            _serviceMock.Setup(s => s.AddSale(productIds, "Test", true)).ReturnsAsync(_saleDomain);

            var result = await _controller.AddSale(productIds, "Test", true);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(_saleDomain, ok.Value);
        }

        [Fact]
        public async Task Update_ReturnsOk_WhenSuccess()
        {   
            _serviceMock.Setup(s => s.Update(_saleDTO)).Returns(Task.CompletedTask);

            var result = await _controller.Update(_saleDTO);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task AlterSaleStatus_ReturnsOk_WhenSuccess()
        {   
            _serviceMock.Setup(s => s.AlterSaleStatus(1)).ReturnsAsync(_saleDomain);

            var result = await _controller.AlterSaleStatus(1);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(_saleDomain, ok.Value);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenSuccess()
        {
            _serviceMock.Setup(s => s.Delete(1)).Returns(Task.CompletedTask);

            var result = await _controller.Delete(1);

            Assert.IsType<OkResult>(result);
        }
    }
}
