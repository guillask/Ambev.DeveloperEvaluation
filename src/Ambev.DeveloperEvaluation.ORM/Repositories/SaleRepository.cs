using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository(DefaultContext context) : ISaleRepository
    {
        DefaultContext _context = context;

        public async Task<Sale?> GetById(int id)
        {
           return await _context.Sales.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Sale>> Filter(DateTime? saleDate, string? customer, string? branch)
        {
            return await _context.Sales
                .Where(x => (!saleDate.HasValue || x.SaleDate == saleDate) &&
                     (string.IsNullOrEmpty(customer) || x.Customer == customer) &&
                     (string.IsNullOrEmpty(branch) || x.Branch == branch))
                .ToListAsync();
        }

        public async Task<Sale> Create(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();

            return sale;
        }

        public async Task Update(Sale sale)
        {
            var salesToUpdate = await _context.Sales.FirstOrDefaultAsync(x => x.Id == sale.Id);

            if (salesToUpdate == null)
                throw new Exception();

            salesToUpdate.SaleDate = sale.SaleDate;
            salesToUpdate.Customer = sale.Customer;
            salesToUpdate.Branch = sale.Branch;
            salesToUpdate.Cancelled = sale.Cancelled;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var sale = await _context.Sales.FirstOrDefaultAsync(x =>x.Id == id);

            if (sale == null)
                throw new Exception();

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
        }
    }
}
