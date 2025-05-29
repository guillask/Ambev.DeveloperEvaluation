using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale?> GetById(int id);
        Task<List<Sale>> Filter(DateTime? saleDate, string? customer, string? branch);
        Task<Sale> Create(Sale sale);
        Task Update(Sale sale);
        Task Delete(int id);
    }
}
