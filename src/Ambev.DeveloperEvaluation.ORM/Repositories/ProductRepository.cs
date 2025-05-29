using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class ProductRepository(DefaultContext context) : IProductRepository
    {
        DefaultContext _context = context;

        public async Task<Product?> GetById(int id)
        {
           return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> Create(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task Update(Product product)
        {
            var productToUpdate = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);

            if (productToUpdate == null)
                throw new Exception();

            productToUpdate.alterationDate = DateTime.Now;
            productToUpdate.Name = product.Name;
            productToUpdate.Price = product.Price;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x =>x.Id == id);

            if (product == null)
                throw new Exception();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
