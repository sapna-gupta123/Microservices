using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SharedService.Dto;
using System.Reflection;
using WebApp.Services.CategoryService;
using WebApp.Services.ProductService;
using WebApp.Services.ProductService.Dto;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var categories = productService.GetProductsAsync();
            if (TempData.ContainsKey("IsSuccess"))
            {
                ViewData["IsSuccess"] = TempData["IsSuccess"];
                ViewData["Message"] = TempData["Message"];

            }
            return View(categories);
        }
        public IActionResult Create(Guid? id)
        {
            var productDto = id.HasValue
                ?  productService.GetProductByIdAsync(id.Value)
                : new ProductDto();

            if (productDto == null)
            {
                return NotFound();
            }

            var categories = categoryService.GetCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", productDto.CategoryId);

            return View(productDto);
        }

        [HttpPost]
        public IActionResult Create(ProductDto productDto)
        {
            ResultDto res = null;
            bool isSuccess = true;
            string Message = "";
            
            if (productDto.Id == Guid.Empty)
            {
                if (ModelState.IsValid)
                {
                    if (productDto.ProductImageFile != null && productDto.ProductImageFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            productDto.ProductImageFile.CopyToAsync(memoryStream);
                            // Convert the image to a byte array
                            var imageBytes = memoryStream.ToArray();
                            // Update the model with image bytes
                            // Assuming you have a way to include this in your ProductDto
                            productDto.ProductImage = imageBytes;
                        }
                    }


                    if (productDto.Id == Guid.Empty)
                    {
                        var categories = categoryService.GetCategoriesAsync().Where(x=> x.Id == productDto.CategoryId).FirstOrDefault();
                        productDto.CategoryName = categories.Name;
                        res = productService.CreateProductAsync(productDto);
                    }
                }
                else
                {
                    res = new ResultDto();
                    res.IsSuccess = false;
                    res.Message = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault().ErrorMessage;
                }
            }
            else
            {
                if (productDto.ProductImageFile != null && productDto.ProductImageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        productDto.ProductImageFile.CopyToAsync(memoryStream);
                        // Convert the image to a byte array
                        var imageBytes = memoryStream.ToArray();
                        // Update the model with image bytes
                        // Assuming you have a way to include this in your ProductDto
                        productDto.ProductImage = imageBytes;
                    }
                }

                var categories = categoryService.GetCategoriesAsync().Where(x => x.Id == productDto.CategoryId).FirstOrDefault();
                productDto.CategoryName = categories.Name;
                res = productService.UpdateProductAsync(productDto);
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

            //ViewBag.Categories = GetCategories(); // Repopulate dropdown on error
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var product = productService.DeleteProductAsync(new Guid(id));
            if (product == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }


    }
}
