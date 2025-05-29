using Ambev.DeveloperEvaluation.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public string? Customer { get; set; }
        public required string Branch { get; set; }
        public required List<ProductsDiscountModel> ProductsDiscount { get; set; }
        public double TotalSaleAmount { get; set; }
        public bool Cancelled { get; set; }
    }
}
