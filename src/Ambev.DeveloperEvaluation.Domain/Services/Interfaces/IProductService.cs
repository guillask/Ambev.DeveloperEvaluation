using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product?> GetById(int id);
        Task<Product> Create(ProductDTO product);
        Task Update(ProductDTO product);
        Task Delete(int id);
    }
}
