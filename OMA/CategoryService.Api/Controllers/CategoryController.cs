using Microsoft.AspNetCore.Mvc;
using CategoryService.Core.Interfaces;
using CategoryService.Api.Filter;
using CategoryService.Model;
using SharedService;
using System.Net;
using CategoryService.Core;

namespace CategoryService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AuthorizeFilter]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly CategoryServiceFactory _categoryServiceFactory;
        public CategoryController(CategoryServiceFactory categoryServiceFactory)
        {
            _categoryServiceFactory = categoryServiceFactory;
        }

        [HttpGet("{categoryType}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories(string categoryType)
        {
            ApiResponse<IEnumerable<Category>> response = new ApiResponse<IEnumerable<Category>>();
            //response.Data = await _categoryService.GetCategoriesAsync();
            response.Data = await _categoryServiceFactory.GetCategoryService(categoryType).GetCategoriesAsync();
            return Ok(response);
        }

        [HttpGet("{categoryType}/{id}")]
        public async Task<ActionResult<Category>> GetCategory(string categoryType, Guid id)
        {
            ApiResponse<Category> response = new ApiResponse<Category>();

            //var category = await _categoryService.GetCategoryByIdAsync(id);
            var category = await _categoryServiceFactory.GetCategoryService(categoryType).GetCategoryByIdAsync(id);
            if (category == null)
            {
                response.Code = (int)HttpStatusCode.NotFound;
                response.Message = "Record not found.";
            }
            else
                response.Data = category;


            return Ok(response);
        }

        [HttpPost("{categoryType}")]
        public async Task<ActionResult<Category>> CreateCategory(string categoryType, Category category)
        {
            ApiResponse<Category> response = new ApiResponse<Category>();
            //var createdCategory = await _categoryService.CreateCategoryAsync(category);
            var createdCategory = await _categoryServiceFactory.GetCategoryService(categoryType).CreateCategoryAsync(category);
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

        [HttpPut("{categoryType}/{id}")]
        public async Task<IActionResult> UpdateCategory(string categoryType, Guid id, Category category)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            if (id != category.Id)
            {
                response.Code = (int)HttpStatusCode.BadRequest;
                response.Data = false;
                response.Message = "Bad request";
                return Ok(response);
            }
            //await _categoryService.UpdateCategoryAsync(category);
            await _categoryServiceFactory.GetCategoryService(categoryType).UpdateCategoryAsync(category);
            response.Data = true;
            response.Message = "Record has been updated.";

            return Ok(response);
        }

        [HttpDelete("{categoryType}/{id}")]
        public async Task<IActionResult> DeleteCategory(string categoryType, Guid id)
        {
            //await _categoryService.DeleteCategoryAsync(id);
            await _categoryServiceFactory.GetCategoryService(categoryType).DeleteCategoryAsync(id);
            ApiResponse<bool> response = new ApiResponse<bool>();
            response.Data = true;
            response.Message = "Record has been deleted.";
            return Ok(response);
        }
    }
}
