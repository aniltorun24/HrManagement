using HrManagement.Application.Departments;
using HrManagement.Domain.Entities;
using HrManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagement.Infrastructure.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _context;

        public DepartmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(CreateDepartmentRequest request)
        {
            var ExistCompanyId = await _context.Companies.AnyAsync(c => c.Id == request.CompanyId);

            if (!ExistCompanyId)
            {
                throw new Exception("Company not found");
            }

            var department = new Department
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CompanyId = request.CompanyId,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();

            return department.Id;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);

            if (department is null)
            {
                throw new Exception("Department not found");
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
            
        }

        public async Task<List<DepartmentResponse>> GetAllAsync()
        {
            var departments = await _context.Departments.Select(d => new DepartmentResponse
            {
                Id = d.Id,
                Name = d.Name,
                CompanyId = d.CompanyId,
                CreatedAt = d.CreatedAt,
            }).ToListAsync();

            return departments;
        }

        public async Task<DepartmentResponse> GetByIdAsync(Guid id)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);

            if (department == null)
            {
                throw new Exception("Department not found");
            }

            return new DepartmentResponse
            {
                Id = department.Id,
                Name = department.Name,
                CompanyId = department.CompanyId,
                CreatedAt = department.CreatedAt
            };
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateDepartmentRequest request)
        {
            var exist = await _context.Departments.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                throw new Exception("Department not found");
            }
            else
            {
                var department = await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);
                department.Name = request.Name;
                department.IsActive = request.IsActive;
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
