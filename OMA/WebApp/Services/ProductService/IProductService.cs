﻿using SharedService.Dto;
using WebApp.Models;
using WebApp.Services.ProductService.Dto;

namespace WebApp.Services.ProductService
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetProductsAsync();
        ProductDto GetProductByIdAsync(Guid id);
        ResultDto CreateProductAsync(ProductDto productDto);
        ResultDto UpdateProductAsync(ProductDto productDto);
        ResultDto DeleteProductAsync(Guid id);
    }
}
