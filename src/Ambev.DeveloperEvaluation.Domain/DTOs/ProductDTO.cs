namespace Ambev.DeveloperEvaluation.Domain.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime? alterationDate { get; set; }
        public required string Name { get; set; }
        public double Price { get; set; }
    }
}
