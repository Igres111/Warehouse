using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity in stock cannot be negative.")]
        public int QuantityInStock { get; set; }
        [Required]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
    }
}
