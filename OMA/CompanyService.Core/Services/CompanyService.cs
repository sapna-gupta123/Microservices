using CompanyService.Core.Interfaces;
using CompanyService.Infrastructure.Interfaces;
using CompanyService.Model;

namespace CompanyService.Core.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await _companyRepository.GetCompaniesAsync();
        }

        public async Task<Company> GetCompanyByIdAsync(Guid id)
        {
            return await _companyRepository.GetCompanyByIdAsync(id);
        }

        public async Task<Company> CreateCompanyAsync(Company company)
        {
            return await _companyRepository.CreateCompanyAsync(company);
        }

        public async Task UpdateCompanyAsync(Company company)
        {
            await _companyRepository.UpdateCompanyAsync(company);
        }

        public async Task DeleteCompanyAsync(Guid id)
        {
            await _companyRepository.DeleteCompanyAsync(id);
        }
    }
}
