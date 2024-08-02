using RestSharp;
using SharedService;
using SharedService.Dto;
using System.Text.Json;
using WebApp.Services.CategoryService.Dto;
using WebApp.Services.ProductService.Dto;

namespace WebApp.Services.ProductService
{
    public class ProductService : IProductService
    {
        public string token = "";
        private readonly RestClient restClient;
        public string UserID = "";
        IHttpContextAccessor htpContextAccessor;


        public ProductService(RestClient restClient, IHttpContextAccessor htpContextAccessor)
        {
            this.restClient = restClient;
            this.htpContextAccessor = htpContextAccessor;
            restClient.Timeout = -1;
            token = htpContextAccessor.HttpContext.Request.Cookies["Auth"];
            UserID = TokenManagerService.GetUserInfo(token).UserID.ToString();
        }
        public ResultDto CreateProductAsync(ProductDto productDto)
        {
            var request = new RestRequest($"Product/Create", Method.POST);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");
            string serializeModel = JsonSerializer.Serialize(productDto);
            request.AddParameter("application/json", serializeModel, ParameterType.RequestBody);
            var response = restClient.Execute(request);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
            var res = JsonSerializer.Deserialize<ApiResponse<ProductDto>>(response.Content, options);
            return new ResultDto
            {
                IsSuccess = res.Code == 200 ? true : false,
                Message = res.Message
            };
            
        }

        public ResultDto DeleteProductAsync(Guid id)
        {
            var request = new RestRequest($"Product/DeleteProduct/{id}", Method.DELETE);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = restClient.Execute(request);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
            var res = JsonSerializer.Deserialize<ApiResponse<bool>>(response.Content, options);
            return new ResultDto
            {
                IsSuccess = res.Data,
                Message = res.Message
            };
            
        }

        public IEnumerable<ProductDto> GetProductsAsync()
        {
            var request = new RestRequest("Product/GetProducts", Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = restClient.Execute(request);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Default is camelCase, change if necessary
                PropertyNameCaseInsensitive = true // Ignore case during deserialization
            };

            var products = JsonSerializer.Deserialize<ApiResponse<IEnumerable<ProductDto>>>(response.Content, options);
            return products.Data;
        }

        public ProductDto GetProductByIdAsync(Guid id)
        {
            var request = new RestRequest($"Product/GetProduct/{id}", Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = restClient.Execute(request);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
            var product = JsonSerializer.Deserialize<ApiResponse<ProductDto>>(response.Content, options);
            return product.Data;

        }

        public ResultDto UpdateProductAsync(ProductDto productDto)
        {
            var request = new RestRequest($"Product/UpdateProduct/{productDto.Id}", Method.PUT);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");
            string serializeModel = JsonSerializer.Serialize(productDto);
            request.AddParameter("application/json", serializeModel, ParameterType.RequestBody);
            var response = restClient.Execute(request);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
            var res = JsonSerializer.Deserialize<ApiResponse<bool>>(response.Content, options);
            return new ResultDto
            {
                IsSuccess = res.Data,
                Message = res.Message
            };
            
        }
    }
}
