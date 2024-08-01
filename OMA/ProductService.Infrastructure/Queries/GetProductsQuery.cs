using MediatR;
using ProductService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
