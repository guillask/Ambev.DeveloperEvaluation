using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProductService _service;

        private readonly ProductDTO _productDTO;
        private readonly Product _product;

        public ProductServiceTests()
        {
            _repositoryMock = new Mock<IProductRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new ProductService(_repositoryMock.Object, _mapperMock.Object);

            _productDTO = new ProductDTO { Id = 1, Name = "Laptop", Price = 100 };
            _product = new Product { Id = 1, Name = "Laptop", Price = 100 };
        }

        [Fact]
        public async Task GetById_ReturnsProduct_WhenFound()
        {
            _repositoryMock.Setup(r => r.GetById(1)).ReturnsAsync(_product);

            var result = await _service.GetById(1);

            Assert.Equal(_product, result);
        }

        [Fact]
        public async Task AddProduct_ReturnsCreatedProduct()
        {
            _mapperMock.Setup(m => m.Map<Product>(_productDTO)).Returns(_product);
            _repositoryMock.Setup(r => r.Create(_product)).ReturnsAsync(_product);

            var result = await _service.AddProduct(_productDTO);

            Assert.Equal(_product, result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedProduct()
        {
            _mapperMock.Setup(m => m.Map<Product>(_productDTO)).Returns(_product);
            _repositoryMock.Setup(r => r.Create(_product)).ReturnsAsync(_product);

            var result = await _service.Create(_productDTO);

            Assert.Equal(_product, result);
        }

        [Fact]
        public async Task Update_CallsRepositoryUpdate()
        {
            _mapperMock.Setup(m => m.Map<Product>(_productDTO)).Returns(_product);

            await _service.Update(_productDTO);

            _repositoryMock.Verify(r => r.Update(_product), Times.Once);
        }

        [Fact]
        public async Task Delete_CallsRepositoryDelete()
        {
            await _service.Delete(1);

            _repositoryMock.Verify(r => r.Delete(1), Times.Once);
        }
    }
}
