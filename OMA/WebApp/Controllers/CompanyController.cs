using Microsoft.AspNetCore.Mvc;
using WebApp.Services.CompanyService;
using WebApp.Services.CompanyService.Dto;
using SharedService;
using SharedService.Dto;

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
            if (TempData.ContainsKey("IsSuccess"))
            {
                ViewData["IsSuccess"] = TempData["IsSuccess"];
                ViewData["Message"] = TempData["Message"];

            }
            return View(categories);
        }
        public IActionResult Create(Guid? id)
        {
            var companyDto = id.HasValue
                ? companyService.GetCompanyByIdAsync(id.Value)
                : new CompanyDto();

            if (companyDto == null)
            {
                companyDto = new CompanyDto();
            }

            return View(companyDto);
        }

        [HttpPost]
        public IActionResult Create(CompanyDto companyDto)
        {
            ResultDto res = null;
            bool isSuccess = true;
            string Message = "";
            if (ModelState.IsValid)
            {
                if (companyDto.Id == Guid.Empty)
                {
                    res = companyService.CreateCompanyAsync(companyDto);
                }
                else
                {
                    res = companyService.UpdateCompanyAsync(companyDto);
                }
            }
            else
            {
                res = new ResultDto();
                res.IsSuccess = false;
                res.Message = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault().ErrorMessage;
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
            var product = companyService.DeleteCompanyAsync(new Guid(id));
            if (product == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }
    }
}
