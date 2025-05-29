using Ambev.DeveloperEvaluation.Domain.Entities;

using Ambev.DeveloperEvaluation.Domain.DTOs;

namespace Ambev.DeveloperEvaluation.Domain.Services.Interfaces
{
    public interface ISaleService
    {
        Task<Sale?> GetById(int id);
        Task<List<Sale>> Filter(DateTime? saleDate, string? customer, string? branch);
        Task<Sale> Create(SaleDTO saleDTO);
        Task<Sale> AddSale(List<int> productsIDs, string customer, bool onlineSale);
        Task Update(SaleDTO saleDTO);
        Task<Sale> AlterSaleStatus(int id);
        Task Delete(int id);
    }
}
