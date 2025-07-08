using Ambev.DeveloperEvaluation.Domain.Consts;
using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Models;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;
using AutoMapper;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Services
{
    public class SaleServiceTests
    {
        private readonly Mock<ISaleRepository> _repositoryMock;
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly SaleService _service;

        private readonly Product _product;
        private readonly SaleDTO _saleDTO;
        private readonly Sale _sale;

        public SaleServiceTests()
        {
            _repositoryMock = new Mock<ISaleRepository>();
            _productServiceMock = new Mock<IProductService>();
            _mapperMock = new Mock<IMapper>();

            _service = new SaleService(_repositoryMock.Object, _productServiceMock.Object, _mapperMock.Object);

            _product = new Product { Id = 1, Name = "Mouse", Price = 100.0 };
            _saleDTO = new SaleDTO
            {
                Customer = "João",
                Branch = SaleBranch.Market,
                SaleDate = DateTime.Now,
                ProductsDiscount = new List<ProductsDiscountModel>()
            };

            _sale = new Sale
            {
                Id = 1,
                Customer = "João",
                Branch = SaleBranch.Market,
                SaleDate = DateTime.Now,
                ProductsDiscount = new List<ProductsDiscountModel>(),
                TotalSaleAmount = 100
            };
        }

        [Fact]
        public async Task GetById_ReturnsSale_WhenExists()
        {
            _repositoryMock.Setup(r => r.GetById(1)).ReturnsAsync(_sale);

            var result = await _service.GetById(1);

            Assert.Equal(_sale, result);
        }

        [Fact]
        public async Task Filter_ReturnsSalesList()
        {
            var salesList = new List<Sale> { _sale };

            _repositoryMock.Setup(r => r.Filter(null, "João", null)).ReturnsAsync(salesList);

            var result = await _service.Filter(null, "João", null);

            Assert.Equal(salesList, result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedSale()
        {
            _mapperMock.Setup(m => m.Map<Sale>(_saleDTO)).Returns(_sale);
            _repositoryMock.Setup(r => r.Create(_sale)).ReturnsAsync(_sale);

            var result = await _service.Create(_saleDTO);

            Assert.Equal(_sale, result);
        }

        [Fact]
        public async Task AddSale_ReturnsCreatedSale_WhenValid()
        {
            var productsIds = new List<int> { 1, 1, 1 }; // 3 unidades do mesmo produto

            _productServiceMock.Setup(p => p.GetById(1)).ReturnsAsync(_product);
            _mapperMock.Setup(m => m.Map<Sale>(It.IsAny<SaleDTO>())).Returns(_sale);
            _repositoryMock.Setup(r => r.Create(It.IsAny<Sale>())).ReturnsAsync(_sale);

            var result = await _service.AddSale(productsIds, "João", false);

            Assert.Equal(_sale, result);
        }

        [Fact]
        public async Task AddSale_ThrowsException_WhenProductCountExceeds20()
        {
            var productIds = Enumerable.Repeat(1, 21).ToList(); // 21 unidades do mesmo produto

            await Assert.ThrowsAsync<Exception>(() => _service.AddSale(productIds, "João", false));
        }

        [Fact]
        public async Task AddSale_ThrowsException_WhenProductNotFound()
        {
            var productIds = new List<int> { 1 };

            _productServiceMock.Setup(p => p.GetById(1)).ReturnsAsync((Product?)null);

            var ex = await Assert.ThrowsAsync<Exception>(() => _service.AddSale(productIds, "João", false));
            Assert.Contains("doesn't exist", ex.Message);
        }

        [Fact]
        public async Task Update_CallsRepositoryUpdate()
        {
            _mapperMock.Setup(m => m.Map<Sale>(_saleDTO)).Returns(_sale);

            await _service.Update(_saleDTO);

            _repositoryMock.Verify(r => r.Update(_sale), Times.Once);
        }

        [Fact]
        public async Task AlterSaleStatus_TogglesCancelled_WhenSaleExists()
        {
            var original = new Sale { Id = 1, Cancelled = false, Branch = "Teste", ProductsDiscount = new List<ProductsDiscountModel> { } };

            _repositoryMock.Setup(r => r.GetById(1)).ReturnsAsync(original);
            _repositoryMock.Setup(r => r.Update(original)).Returns(Task.CompletedTask);

            var result = await _service.AlterSaleStatus(1);

            Assert.True(result.Cancelled);
            _repositoryMock.Verify(r => r.Update(original), Times.Once);
        }

        [Fact]
        public async Task AlterSaleStatus_ThrowsException_WhenSaleNotFound()
        {
            _repositoryMock.Setup(r => r.GetById(999)).ReturnsAsync((Sale?)null);

            var ex = await Assert.ThrowsAsync<Exception>(() => _service.AlterSaleStatus(999));
            Assert.Contains("doesn't exist", ex.Message);
        }

        [Fact]
        public async Task Delete_CallsRepositoryDelete()
        {
            await _service.Delete(1);

            _repositoryMock.Verify(r => r.Delete(1), Times.Once);
        }
    }
}
