using CompanyService.Api.Filter;
using CompanyService.Core.Interfaces;
using CompanyService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedService;
using System.Net;

namespace CompanyService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AuthorizeFilter]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            ApiResponse<IEnumerable<Company>> response = new ApiResponse<IEnumerable<Company>>();
            response.Data = await _companyService.GetCompaniesAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(Guid id)
        {
            ApiResponse<Company> response = new ApiResponse<Company>();
            var category = await _companyService.GetCompanyByIdAsync(id);
            if (category == null)
            {
                response.Code = (int)HttpStatusCode.NotFound;
                response.Message = "Record not found.";
            }
            else
                response.Data = category;


            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Company>> CreateCompany(Company company)
        {
            ApiResponse<Company> response = new ApiResponse<Company>();
            //var createdCategory = await _categoryService.CreateCategoryAsync(category);
            var createdCategory =  await _companyService.CreateCompanyAsync(company);
            if (createdCategory.Id != Guid.Empty)
            {
                response.Data = createdCategory;
                response.Message = "Record has been created.";
            }
            else
            {
                response.Code = (int)HttpStatusCode.InternalServerError;
                response.Message = "Some error occured while creating a record.";
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(Guid id, Company company)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            if (id != company.Id)
            {
                response.Code = (int)HttpStatusCode.BadRequest;
                response.Data = false;
                response.Message = "Bad request";
                return Ok(response);
            }
            //await _categoryService.UpdateCategoryAsync(category);
            await _companyService.UpdateCompanyAsync(company);
            response.Data = true;
            response.Message = "Record has been updated.";

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            await _companyService.DeleteCompanyAsync(id);
            ApiResponse<bool> response = new ApiResponse<bool>();
            response.Data = true;
            response.Message = "Record has been deleted.";
            return Ok(response);
        }
    }
}
