﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Services.ProductService.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public Guid CategoryId { get; set; }

        public string? CategoryName { get; set; }

        [Required]
        public string Manufacturer { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public IFormFile? ProductImageFile { get; set; }
        public byte[]? ProductImage { get; set; }
    }
}
