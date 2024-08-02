using RestSharp;
using SharedService;
using SharedService.Dto;
using System.Text.Json;
using WebApp.Models;
using WebApp.Services.CategoryService.Dto;
using WebApp.Services.CompanyService.Dto;
using WebApp.Services.ProductService.Dto;

namespace WebApp.Services.CompanyService
{
    public class CompanyService : ICompanyService
    {

        public string token = "";
        private readonly RestClient restClient;
        public string UserID = "";
        IHttpContextAccessor htpContextAccessor;

        public CompanyService(RestClient restClient, IHttpContextAccessor htpContextAccessor)
        {
            this.restClient = restClient;
            this.htpContextAccessor = htpContextAccessor;
            restClient.Timeout = -1;
            token = htpContextAccessor.HttpContext.Request.Cookies["Auth"];
            UserID = TokenManagerService.GetUserInfo(token).UserID.ToString();
        }
        public ResultDto CreateCompanyAsync(CompanyDto companyDto)
        {
            var request = new RestRequest($"Company/CreateCompany", Method.POST);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");
            string serializeModel = JsonSerializer.Serialize(companyDto);
            request.AddParameter("application/json", serializeModel, ParameterType.RequestBody);
            var response = restClient.Execute(request);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
            var res = JsonSerializer.Deserialize<ApiResponse<CompanyDto>>(response.Content, options);
            return new ResultDto
            {
                IsSuccess = res.Code == 200 ? true : false,
                Message = res.Message
            };
            //return Utilities.GetResponseStatusCode(response);
        }

        public ResultDto DeleteCompanyAsync(Guid id)
        {
            var request = new RestRequest($"Company/DeleteCompany/{id}", Method.DELETE);
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

        public IEnumerable<CompanyDto> GetCompaniesAsync()
        {
            var request = new RestRequest("Company/GetCompanies", Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = restClient.Execute(request);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Default is camelCase, change if necessary
                PropertyNameCaseInsensitive = true // Ignore case during deserialization
            };

            var categories = JsonSerializer.Deserialize<ApiResponse<IEnumerable<CompanyDto>>>(response.Content, options);
            return categories.Data;

        }

        public CompanyDto GetCompanyByIdAsync(Guid id)
        {
            var request = new RestRequest($"Company/GetCompany/{id}", Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = restClient.Execute(request);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
            var company = JsonSerializer.Deserialize<ApiResponse<CompanyDto>>(response.Content, options);
            return company.Data;
        }

        public ResultDto UpdateCompanyAsync(CompanyDto companyDto)
        {
            var request = new RestRequest($"Company/UpdateCompany/{companyDto.Id}", Method.PUT);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");
            string serializeModel = JsonSerializer.Serialize(companyDto);
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
