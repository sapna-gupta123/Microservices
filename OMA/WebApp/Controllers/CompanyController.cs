using Microsoft.AspNetCore.Mvc;
using WebApp.Services.ProductService.Dto;
using WebApp.Services.ProductService;
using WebApp.Services.CompanyService;
using WebApp.Services.CompanyService.Dto;

namespace WebApp.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService companyService;
        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }
        public IActionResult Index()
        {
            var categories = companyService.GetCompaniesAsync();
            return View(categories);
        }
        public IActionResult Create(Guid? id)
        {
            var companyDto = new CompanyDto();

            //var companyDto = id.HasValue
            //    ? await productService.GetProductByIdAsync(id.Value)
            //    : new companyDto();

            if (companyDto == null)
            {
                return NotFound();
            }

            //ViewBag.Categories = GetCategories(); // Populate dropdown
            return View(companyDto);
        }

        [HttpPost]
        public IActionResult Create(CompanyDto companyDto)
        {
            if (companyDto.Id != Guid.Empty)
            {
                if (ModelState.IsValid)
                {
                    if (companyDto.Id == Guid.Empty)
                    {
                        //await productService.CreateProductAsync(companyDto);
                    }
                    else
                    {
                        //await productService.UpdateProductAsync(companyDto);
                    }

                    return RedirectToAction(nameof(Index));
                }
            }

            //ViewBag.Categories = GetCategories(); // Repopulate dropdown on error
            return View(companyDto);
        }
    }
}
