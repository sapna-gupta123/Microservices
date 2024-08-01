using MediatR;
using ProductService.Infrastructure.Commands;
using ProductService.Infrastructure.Data;
using ProductService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly ProductContext _context;

        public UpdateProductCommandHandler(ProductContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            product.Name = request.Name;
            product.CategoryName = request.CategoryName;
            product.Manufacturer = request.Manufacturer;
            product.Quantity = request.Quantity;
            product.Price = request.Price;
            product.ProductImage = request.ProductImage;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return product;
        }
    }
}
