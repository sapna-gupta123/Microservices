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
            var request = new RestRequest($"/api/Category", Method.POST);
            request.AddHeader("token", token);
            request.AddHeader("Content-Type", "application/json");
            string serializeModel = JsonSerializer.Serialize(categoryDto);
            request.AddParameter("application/json", serializeModel, ParameterType.RequestBody);
            var response = restClient.Execute(request);
            return Utilities.GetResponseStatusCode(response);
        }

        public ResultDto DeleteCategoryAsync(Guid id)
        {
            var request = new RestRequest($"/api/Category/DeleteCategory?id={id}", Method.DELETE);
            request.AddHeader("token", token);
            IRestResponse response = restClient.Execute(request);
            return Utilities.GetResponseStatusCode(response);
        }

        public IEnumerable<CategoryDto> GetCategoriesAsync()
        {
            var request = new RestRequest($"/api/Category", Method.GET);
            request.AddHeader("token", token);
            IRestResponse response = restClient.Execute(request);
            var basket = JsonSerializer.Deserialize<IEnumerable<CategoryDto>>(response.Content);
            return basket;
        }

        public ResultDto GetCategoryByIdAsync(Guid id)
        {
            var request = new RestRequest($"/api/Category/{id}", Method.PUT);
            request.AddHeader("token", token);
            IRestResponse response = restClient.Execute(request);
            return Utilities.GetResponseStatusCode(response);
        }

        public ResultDto UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var request = new RestRequest($"/api/Category", Method.PUT);
            request.AddHeader("token", token);
            IRestResponse response = restClient.Execute(request);
            return Utilities.GetResponseStatusCode(response);
        }
       
    }
}
