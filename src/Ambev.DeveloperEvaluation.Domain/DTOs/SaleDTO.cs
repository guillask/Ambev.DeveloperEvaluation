using Ambev.DeveloperEvaluation.Domain.Models;

namespace Ambev.DeveloperEvaluation.Domain.DTOs
{
    public class SaleDTO
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public string? Customer { get; set; }
        public required string Branch { get; set; }
        public required List<ProductsDiscountModel> ProductsDiscount { get; set; }
        public double TotalSaleAmount { get; set; }
        public bool Cancelled { get; set; }
    }
}
