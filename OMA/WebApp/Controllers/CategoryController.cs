using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SharedService;
using SharedService.Dto;
using WebApp.Services.CategoryService;
using WebApp.Services.CategoryService.Dto;
using WebApp.Services.ProductService.Dto;

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

            if(TempData.ContainsKey("IsSuccess"))
            {
                ViewData["IsSuccess"] = TempData["IsSuccess"];
                ViewData["Message"] = TempData["Message"];

            }

            return View(categories);
        }

        public IActionResult Create(Guid? id)
        {
            var categoryDto = id.HasValue
                ? categoryService.GetCategoryByIdAsync(id.Value)
                : new CategoryDto();

            if (categoryDto == null)
            {
                return NotFound();
            }
            ViewBag.CategoryTypes = Enum.GetValues(typeof(CategoryType))
                            .Cast<CategoryType>()
                            .Select(e => new SelectListItem
                            {
                                Value = e.ToString(),
                                Text = e.ToString()
                            });
            return View(categoryDto);
        }

        [HttpPost]
        public IActionResult Create(CategoryDto categoryDto)
        {
            ResultDto res = null;
            bool isSuccess = true;
            string Message = "";
            if (categoryDto.Id == Guid.Empty)
            {
                if (ModelState.IsValid)
                {
                    res = categoryService.CreateCategoryAsync(categoryDto);
                    
                }
                else
                {
                    res= new ResultDto();
                    res.IsSuccess = false;
                    res.Message = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault().ErrorMessage;
                }
            }
            else
            {
                res = categoryService.UpdateCategoryAsync(categoryDto);
            }

            if (res != null)
            {
                TempData["IsSuccess"] = res.IsSuccess;
                TempData["Message"] = res.Message;

            }
            else
            {

                TempData["IsSuccess"] = false;
                TempData["Message"] = "Some internal error occurs";
            }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var product = categoryService.DeleteCategoryAsync(new Guid(id));
            if (product == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}
