using Microsoft.Extensions.Options;
using RestSharp;
using SharedService;
using SharedService.Dto;
using System.Text.Json;
using WebApp.Models;
using WebApp.Services.CategoryService.Dto;

namespace WebApp.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        public string token = "";
        private readonly RestClient restClient;
        public string UserID = "";
        IHttpContextAccessor htpContextAccessor;


        public CategoryService(RestClient restClient, IHttpContextAccessor htpContextAccessor)
        {
            this.restClient = restClient;
            this.htpContextAccessor = htpContextAccessor;
            restClient.Timeout = -1;
            token = htpContextAccessor.HttpContext.Request.Cookies["Auth"];
            UserID = TokenManagerService.GetUserInfo(token).UserID.ToString();
        }
        public ResultDto CreateCategoryAsync(CategoryDto categoryDto)
        {
            var request = new RestRequest($"Category/CreateCategory/{categoryDto.CategoryType}", Method.POST);
            request.AddHeader("Authorization","Bearer " + token);
            request.AddHeader("Content-Type", "application/json");
            string serializeModel = JsonSerializer.Serialize(categoryDto);
            request.AddParameter("application/json", serializeModel, ParameterType.RequestBody);
            var response = restClient.Execute(request);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
            var res = JsonSerializer.Deserialize<ApiResponse<CategoryDto>>(response.Content, options);
            return new ResultDto
            {
                IsSuccess = res.Code == 200 ? true: false,
                Message = res.Message
            };
            //return Utilities.GetResponseStatusCode(response);
        }

        public ResultDto DeleteCategoryAsync(Guid id)
        {
            var request = new RestRequest($"Category/DeleteCategory/Default/{id}", Method.DELETE);
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
            //return Utilities.GetResponseStatusCode(response);
        }

        public IEnumerable<CategoryDto> GetCategoriesAsync()
        {
            var request = new RestRequest("Category/GetCategories/Default", Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = restClient.Execute(request);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Default is camelCase, change if necessary
                PropertyNameCaseInsensitive = true // Ignore case during deserialization
            };

            var categories = JsonSerializer.Deserialize<ApiResponse<IEnumerable<CategoryDto>>> (response.Content, options);
            return categories.Data;
        }

        public CategoryDto GetCategoryByIdAsync(Guid id)
        {
            var request = new RestRequest($"Category/GetCategory/Default/{id}", Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = restClient.Execute(request);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, 
                PropertyNameCaseInsensitive = true
            };
            var category = JsonSerializer.Deserialize<ApiResponse<CategoryDto>>(response.Content, options);
            return category.Data;
        }

        public ResultDto UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var request = new RestRequest($"Category/UpdateCategory/{categoryDto.CategoryType}/{categoryDto.Id}", Method.PUT);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");
            string serializeModel = JsonSerializer.Serialize(categoryDto);
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
