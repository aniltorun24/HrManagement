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
    }
}
