﻿using SharedService.Dto;
using WebApp.Services.CompanyService.Dto;

namespace WebApp.Services.CompanyService
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetCompaniesAsync();
        CompanyDto GetCompanyByIdAsync(Guid id);
        ResultDto CreateCompanyAsync(CompanyDto companyDto);
        ResultDto UpdateCompanyAsync(CompanyDto companyDto);
        ResultDto DeleteCompanyAsync(Guid id);
    }
}
