using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Services.ProductService;
using WebApp.Services.ProductService.Dto;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        public IActionResult Index()
        {
            var categories = productService.GetProductsAsync();
            return View(categories);
        }
        public IActionResult Create(Guid? id)
        {
            var productDto = new ProductDto();

            //var productDto = id.HasValue
            //    ? await productService.GetProductByIdAsync(id.Value)
            //    : new ProductDto();

            if (productDto == null)
            {
                return NotFound();
            }

            //ViewBag.Categories = GetCategories(); // Populate dropdown
            return View(productDto);
        }

        [HttpPost]
        public IActionResult Create(ProductDto productDto)
        {
            if (productDto.Id != Guid.Empty)
            {
                if (ModelState.IsValid)
                {
                    if (productDto.ProductImage != null && productDto.ProductImage.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            //await productDto.ProductImage.CopyToAsync(memoryStream);
                            //productDto.ProductImage = memoryStream.ToArray();
                        }
                    }

                    if (productDto.Id == Guid.Empty)
                    {
                        //await productService.CreateProductAsync(productDto);
                    }
                    else
                    {
                        //await productService.UpdateProductAsync(productDto);
                    }

                    return RedirectToAction(nameof(Index));
                }
            }

            //ViewBag.Categories = GetCategories(); // Repopulate dropdown on error
            return View(productDto);
        }

        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var product = await productService.GetProductByIdAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        //[HttpPost, ActionName("Delete")]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    await productService.DeleteProductAsync(id);
        //    return RedirectToAction(nameof(Index));
        //}

        //private IEnumerable<SelectListItem> GetCategories()
        //{
        //    //var categories = productService.GetCategories();
        //    return categories.Select(c => new SelectListItem
        //    {
        //        Value = c.Id.ToString(),
        //        Text = c.Name
        //    });
        //}
    }
}
