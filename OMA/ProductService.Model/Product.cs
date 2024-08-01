using System.ComponentModel.DataAnnotations;

namespace ProductService.Model
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; } = null!;
        [Required]
        public string Manufacturer { get; set; } = null!;
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        public byte[] ProductImage { get; set; } = null!; // JPEG only
    }
}
