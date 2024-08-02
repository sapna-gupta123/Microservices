using CategoryService.Core.Interfaces;
using CategoryService.Infrastructure.Interfaces;
using CategoryService.Model;
using SharedService;

namespace CategoryService.Core.Services
{
    public class EelectronicCategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public EelectronicCategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _categoryRepository.GetCategoriesAsync(CategoryType.Electronics.ToString());
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            category.CategoryType = CategoryType.Electronics.ToString();
            return await _categoryRepository.CreateCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            category.CategoryType = CategoryType.Electronics.ToString();
            await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}
