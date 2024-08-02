using CompanyService.Infrastructure.Data;
using CompanyService.Infrastructure.Interfaces;
using CompanyService.Model;
using Microsoft.EntityFrameworkCore;

namespace CompanyService.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompanyContext _context;

        public CompanyRepository(CompanyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetCompanyByIdAsync(Guid id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public async Task<Company> CreateCompanyAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task UpdateCompanyAsync(Company company)
        {
            var companyDto = await _context.Companies.FindAsync(company.Id);
            if (companyDto != null)
            {
                companyDto.Name = company.Name;
                companyDto.StreetAddress = company.StreetAddress;
                companyDto.City = company.City;
                companyDto.State = company.State;
                companyDto.PostalAddress = company.PostalAddress;
                companyDto.Zip = company.Zip;
                companyDto.ContactNumber = company.ContactNumber;

                _context.Entry(companyDto).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCompanyAsync(Guid id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
            }
        }
    }
}
