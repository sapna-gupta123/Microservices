using CategoryService.Core.Interfaces;
using CategoryService.Infrastructure.Interfaces;
using CategoryService.Model;
using SharedService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryService.Core.Services
{
    public class HomeApplianceCategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public HomeApplianceCategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _categoryRepository.GetCategoriesAsync(CategoryType.HomeAppliances.ToString());
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            category.CategoryType = CategoryType.HomeAppliances.ToString();
            return await _categoryRepository.CreateCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            category.CategoryType = CategoryType.HomeAppliances.ToString();
            await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
        }
    }
}