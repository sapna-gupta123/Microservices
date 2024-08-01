using SharedService.Dto;
using WebApp.Models;
using WebApp.Services.CategoryService.Dto;

namespace WebApp.Services.CategoryService
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetCategoriesAsync();
        ResultDto GetCategoryByIdAsync(Guid id);
        ResultDto CreateCategoryAsync(CategoryDto categoryDto);
        ResultDto UpdateCategoryAsync(CategoryDto categoryDto);
        ResultDto DeleteCategoryAsync(Guid id);
    }
}
