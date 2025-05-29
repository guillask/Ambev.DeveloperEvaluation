using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Models
{
    public class ProductsDiscountModel
    {
        public int ProductId { get; set; }
        public int Units { get; set; }
        public double UnitPrice { get; set; }
        public double SaleAmount { get; set; }
        public int Discount { get; set; }
    }
}
