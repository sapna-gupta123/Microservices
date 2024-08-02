using MediatR;
using ProductService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Commands
{
    public class CreateProductCommand : IRequest<Product>
    {
        public string Name { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string Manufacturer { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public byte[] ProductImage { get; set; } = null!;
    }
}
