namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime? alterationDate { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
