using Microsoft.AspNetCore.Mvc;
using WebApp.Services.CategoryService;
using WebApp.Services.CategoryService.Dto;

namespace WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var categories = categoryService.GetCategoriesAsync();
            return View(categories);
        }

        public IActionResult Create(Guid? id)
        {
            var categoryDto = new CategoryDto();

            //var categoryDto = id.HasValue
            //    ? await productService.GetProductByIdAsync(id.Value)
            //    : new categoryDto();

            if (categoryDto == null)
            {
                return NotFound();
            }

            //ViewBag.Categories = GetCategories(); // Populate dropdown
            return View(categoryDto);
        }

        [HttpPost]
        public IActionResult Create(CategoryDto categoryDto)
        {
            if (categoryDto.Id != Guid.Empty)
            {
                if (ModelState.IsValid)
                {
                    if (categoryDto.Id == Guid.Empty)
                    {
                        //await productService.CreateProductAsync(categoryDto);
                    }
                    else
                    {
                        //await productService.UpdateProductAsync(categoryDto);
                    }

                    return RedirectToAction(nameof(Index));
                }
            }

            //ViewBag.Categories = GetCategories(); // Repopulate dropdown on error
            return View(categoryDto);
        }
    }
}
