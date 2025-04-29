namespace AdminPanel.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int QuantityInStock { get; set; }
        public decimal Price { get; set; }
    }
}
