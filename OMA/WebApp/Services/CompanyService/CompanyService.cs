using RestSharp;
using SharedService;
using SharedService.Dto;
using System.Text.Json;
using WebApp.Models;
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
            var request = new RestRequest($"/api/Company", Method.POST);
            request.AddHeader("token", token);
            request.AddHeader("Content-Type", "application/json");
            string serializeModel = JsonSerializer.Serialize(companyDto);
            request.AddParameter("application/json", serializeModel, ParameterType.RequestBody);
            var response = restClient.Execute(request);
            return Utilities.GetResponseStatusCode(response);
        }

        public ResultDto DeleteCompanyAsync(Guid id)
        {
            var request = new RestRequest($"/api/Product/Company?id={id}", Method.DELETE);
            request.AddHeader("token", token);
            IRestResponse response = restClient.Execute(request);
            return Utilities.GetResponseStatusCode(response);
        }

        public Task<IEnumerable<CompanyDto>> GetCompaniesAsync()
        {
            var request = new RestRequest($"/api/Company", Method.GET);
            request.AddHeader("token", token);
            IRestResponse response = restClient.Execute(request);
            var basket = JsonSerializer.Deserialize<Task<IEnumerable<CompanyDto>>>(response.Content);
            return basket;
        }

        public Task<CompanyDto> GetCompanyByIdAsync(Guid id)
        {
            var request = new RestRequest($"/api/Company/{id}", Method.PUT);
            request.AddHeader("token", token);
            IRestResponse response = restClient.Execute(request);
            var company = JsonSerializer.Deserialize<Task<CompanyDto>>(response.Content);
            return company;
        }

        public ResultDto UpdateCompanyAsync(CompanyDto companyDto)
        {
            var request = new RestRequest($"/api/Company", Method.PUT);
            request.AddHeader("token", token);
            IRestResponse response = restClient.Execute(request);
            return Utilities.GetResponseStatusCode(response);
        }
    }
}
