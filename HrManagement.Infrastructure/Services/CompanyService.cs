using HrManagement.Application.Companies;
using HrManagement.Domain.Entities;
using HrManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HrManagement.Infrastructure.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext _context;

        public CompanyService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> CreateAsync(CreateCompanyRequest request)
        {
            var exists = await _context.Companies
            .AnyAsync(x => x.Name == request.Name);

            if (exists)
            {
                // burada exception veya uygun bir hata sonucu döndür
            }

            var company = new 
                Company
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company.Id;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            var existCompany = _context.Companies.FirstOrDefault(x => x.Id == id);

            if (existCompany != null)
            {
                _context.Companies.Remove(existCompany);
                _context.SaveChanges();
                return Task.FromResult(true);
            }

            else
            {
                throw new InvalidOperationException("Company not found");
            }
        }

        public Task<List<CompanyResponse>> GetAllAsync()
        {
            var existingCompanies = _context.Companies
                .Select(x => new CompanyResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    IsActive = x.IsActive
                })
                .ToListAsync();

            return existingCompanies;
        }

        public async Task<CompanyResponse> GetByIdAsync(Guid id)
        {
            var existCompany = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (existCompany == null)
            {
                throw new InvalidOperationException("Company not found");
            }

            return new CompanyResponse
            {
                Id = existCompany.Id,
                Name = existCompany.Name,
                CreatedAt = existCompany.CreatedAt,
                IsActive = existCompany.IsActive
            };

            
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateCompanyAsync request)
        {
            var existCompany = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);

            if (existCompany == null)
            {
                throw new InvalidOperationException("Company not found");
            }

            existCompany.Name = request.Name;
            existCompany.IsActive = request.IsActive;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
