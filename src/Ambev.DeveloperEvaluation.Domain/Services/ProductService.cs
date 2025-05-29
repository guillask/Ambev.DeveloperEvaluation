using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public class ProductService(IProductRepository repository, IMapper mapper) : IProductService
    {
        IProductRepository _repository = repository;
        IMapper _mapper = mapper;

        public async Task<Product?> GetById(int id)
        {
            var product = await _repository.GetById(id);
            return product;
        }

        public async Task<Product> AddProduct(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);


            return await _repository.Create(product);
        }

        public async Task<Product> Create(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            return await _repository.Create(product);
        }

        public async Task Update(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            await _repository.Update(product);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}
