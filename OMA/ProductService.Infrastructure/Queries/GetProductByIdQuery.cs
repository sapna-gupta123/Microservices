using MediatR;
using ProductService.Model;

namespace ProductService.Infrastructure.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public Guid Id { get; set; }
    }
}
