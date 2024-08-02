using CategoryService.Infrastructure.Data;
using CategoryService.Infrastructure.Interfaces;
using CategoryService.Model;
using Microsoft.EntityFrameworkCore;

namespace CategoryService.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryContext _context;

        public CategoryRepository(CategoryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(String categoryType = null)
        {
            if (categoryType == null)
            {
                return await _context.Categories.ToListAsync();
            }
            else
            {
                return await _context.Categories.Where(x => x.CategoryType == categoryType).ToListAsync();
            }

        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task UpdateCategoryAsync(Category category)
        {
                var categoryDto = await _context.Categories.FindAsync(category.Id);
                if (categoryDto != null)
                {
                    categoryDto.Name = category.Name;

                    _context.Entry(categoryDto).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }

}
