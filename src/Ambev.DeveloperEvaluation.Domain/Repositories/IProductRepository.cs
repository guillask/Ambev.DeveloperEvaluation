using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetById(int id);
        Task<Product> Create(Product product);
        Task Update(Product product);
        Task Delete(int id);
    }
}
