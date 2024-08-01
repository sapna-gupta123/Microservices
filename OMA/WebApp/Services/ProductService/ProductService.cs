using RestSharp;
using SharedService;
using System.Text.Json;
using WebApp.Models;
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
            //token = htpContextAccessor.HttpContext.Request.Cookies["Auth"];
            //UserID = TokenManagerService.GetUserInfo(token).UserID.ToString();
        }
        public ResultDto CreateProductAsync(ProductDto productDto)
        {
            var request = new RestRequest($"/api/Product", Method.POST);
            request.AddHeader("token", token);
            request.AddHeader("Content-Type", "application/json");
            string serializeModel = JsonSerializer.Serialize(productDto);
            request.AddParameter("application/json", serializeModel, ParameterType.RequestBody);
            var response = restClient.Execute(request);
            return GetResponseStatusCode(response);
        }

        public ResultDto DeleteProductAsync(Guid id)
        {
            var request = new RestRequest($"/api/Product/DeleteCategory?id={id}", Method.DELETE);
            request.AddHeader("token", token);
            IRestResponse response = restClient.Execute(request);
            return GetResponseStatusCode(response);
        }

        public IEnumerable<ProductDto> GetProductsAsync()
        {
            var request = new RestRequest($"/api/Product", Method.GET);
            request.AddHeader("token", token);
            IRestResponse response = restClient.Execute(request);
            var basket = JsonSerializer.Deserialize<IEnumerable<ProductDto>>(response.Content);
            return basket;
        }

        public ResultDto GetProductByIdAsync(Guid id)
        {
            var request = new RestRequest($"/api/Product/{id}", Method.PUT);
            request.AddHeader("token", token);
            IRestResponse response = restClient.Execute(request);
            return GetResponseStatusCode(response);
        }

        public ResultDto UpdateProductAsync(ProductDto productDto)
        {
            var request = new RestRequest($"/api/Product", Method.PUT);
            request.AddHeader("token", token);
            IRestResponse response = restClient.Execute(request);
            return GetResponseStatusCode(response);
        }
        private static ResultDto GetResponseStatusCode(IRestResponse response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new ResultDto
                {
                    IsSuccess = true,
                };
            }
            else
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = response.ErrorMessage
                };
            }
        }
    }
}
