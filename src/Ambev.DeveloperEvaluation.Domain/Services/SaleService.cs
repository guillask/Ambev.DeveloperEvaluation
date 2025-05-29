using Ambev.DeveloperEvaluation.Domain.Consts;
using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Models;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public class SaleService(ISaleRepository repository, IProductService productService, IMapper mapper) : ISaleService
    {
        ISaleRepository _repository = repository;
        IProductService _productService = productService;
        IMapper _mapper = mapper;

        public async Task<Sale?> GetById(int id)
        {
            var sale = await _repository.GetById(id);
            return sale;
        }
        public async Task<List<Sale>> Filter(DateTime? saleDate, string? customer, string? branch)
        {
            var sales = await _repository.Filter(saleDate, customer, branch);
            return sales;
        }

        public async Task<Sale> Create(SaleDTO saleDTO)
        {
            var sale = _mapper.Map<Sale>(saleDTO);
            return await _repository.Create(sale);
        }

        public async Task<Sale> AddSale(List<int> productsIDs, string customer, bool onlineSale)
        {
            var groups = productsIDs.GroupBy(x => x);

            if (groups.Any(x => x.Count() > 20))
                throw new Exception("Maximum of 20 items per product.");

            var saleDTO = new SaleDTO
            {
                SaleDate = DateTime.Now,
                Customer = customer,
                Branch = onlineSale ? SaleBranch.Online : SaleBranch.Market,
                ProductsDiscount = []
            };

            double totalSaleAmount = 0;
           
            foreach (var group in groups)
            {
                var product = await _productService.GetById(group.Key);

                if (product == null)
                    throw new Exception($"The product of ID {group.Key} doesn't exist.");

                var units = group.Count();

                var discount = VerifyDiscount(group.ToList());

                var saleAmount = GetSaleAmount(product, units, discount);

                totalSaleAmount += saleAmount;

                var productsDiscount = new ProductsDiscountModel
                {
                    ProductId = product.Id, 
                    Units = units, 
                    UnitPrice = product.Price,
                    SaleAmount = saleAmount,
                    Discount = discount
                };

                saleDTO.ProductsDiscount.Add(productsDiscount);
            }

            saleDTO.TotalSaleAmount = totalSaleAmount;

            var sale = _mapper.Map<Sale>(saleDTO);
            return await _repository.Create(sale);
        }

        public async Task Update(SaleDTO saleDTO)
        {
            var sale = _mapper.Map<Sale>(saleDTO);
            await _repository.Update(sale);
        }

        public async Task<Sale> AlterSaleStatus(int id)
        {
            var sale = await _repository.GetById(id);

            if (sale == null)
                throw new Exception($"The sale of ID {id} doesn't exist.");

            sale.Cancelled = !sale.Cancelled;

            await _repository.Update(sale);

            return sale;
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        private int VerifyDiscount(List<int> group)
        {
            var count = group.Count();
            var discount = 0;

            if (count > 4 && count < 10)
                discount = 10;
            else if (count >= 10 && count <= 20)
                discount = 20;

            return discount;
        }

        private double GetSaleAmount(Product product, int units, double discount)
        {
            var totalSaleAmount = product.Price * units;

            discount = discount / 100;

            totalSaleAmount = totalSaleAmount - (totalSaleAmount * discount);

            return totalSaleAmount;
        }
    }
}
