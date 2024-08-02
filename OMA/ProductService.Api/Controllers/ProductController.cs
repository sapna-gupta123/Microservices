using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Api.Filter;
using ProductService.Infrastructure.Commands;
using ProductService.Infrastructure.Queries;
using ProductService.Model;
using SharedService;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProductService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AuthorizeFilter]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateProductCommand>> Create(CreateProductCommand command)
        {
           
            ApiResponse<Product> response = new ApiResponse<Product>();
            var product = await _mediator.Send(command);
            if (product.Id != Guid.Empty)
            {
                response.Data = product;
                response.Message = "Record has been created.";
            }
            else
            {
                response.Code = (int)HttpStatusCode.InternalServerError;
                response.Message = "Some error occured while creating a record.";
            }
            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(Guid id)
        {
            ApiResponse<Product> response = new ApiResponse<Product>();
            var query = new GetProductByIdQuery { Id = id };
            var product = await _mediator.Send(query);
            if (product == null)
            {
                response.Code = (int)HttpStatusCode.NotFound;
                response.Message = "Record not found.";
            }
            else
                response.Data = product;

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<Product>> GetProducts()
        {
            var query = new GetProductsQuery();
            ApiResponse<IEnumerable<Product>> response = new ApiResponse<IEnumerable<Product>>();
            response.Data = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductCommand command)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();
            
            if (id != command.Id)
            {
                response.Code = (int)HttpStatusCode.BadRequest;
                response.Data = false;
                response.Message = "Bad request";
                return Ok(response);
            }
            var product = await _mediator.Send(command);
            response.Data = true;
            response.Message = "Record has been updated.";

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var command = new DeleteProductCommand { Id = id };
            ApiResponse<bool> response = new ApiResponse<bool>();
            try
            {
                await _mediator.Send(command);
                
                response.Data = true;
                response.Message = "Record has been deleted.";
                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                response.Code = (int)HttpStatusCode.NotFound;
                response.Data = false;
                response.Message = "Record not found.";
                return Ok(response);
            }
        }
    }
}
