using HrManagement.Application.Employees;
using HrManagement.Application.Common.Exceptions;
using HrManagement.Domain.Entities;
using HrManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HrManagement.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(CreateEmployeeRequest request)
        {
            await ValidatePositionAsync(request.PositionId);

            var normalizedEmail = NormalizeEmail(request.Email);
            var emailExists = await _context.Employees
                .AnyAsync(x => x.Email.ToLower() == normalizedEmail);

            if (emailExists)
            {
                throw new ConflictException("Employee email already exists");
            }

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                Email = normalizedEmail,
                Phone = request.Phone?.Trim(),
                PositionId = request.PositionId,
                HireDate = request.HireDate.ToUniversalTime(),
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return employee.Id;
        }

        public async Task<List<EmployeeResponse>> GetAllAsync()
        {
            return await _context.Employees
                .AsNoTracking()
                .Select(x => new EmployeeResponse
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Phone = x.Phone,
                    PositionId = x.PositionId,
                    HireDate = x.HireDate,
                    CreatedAt = x.CreatedAt,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<EmployeeResponse> GetByIdAsync(Guid id)
        {
            var employee = await _context.Employees
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new EmployeeResponse
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Phone = x.Phone,
                    PositionId = x.PositionId,
                    HireDate = x.HireDate,
                    CreatedAt = x.CreatedAt,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();

            if (employee is null)
            {
                throw new NotFoundException("Employee not found");
            }

            return employee;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateEmployeeRequest request)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(x => x.Id == id);

            if (employee is null)
            {
                throw new NotFoundException("Employee not found");
            }

            await ValidatePositionAsync(request.PositionId);

            var normalizedEmail = NormalizeEmail(request.Email);
            var emailExists = await _context.Employees
                .AnyAsync(x => x.Id != id && x.Email.ToLower() == normalizedEmail);

            if (emailExists)
            {
                throw new ConflictException("Employee email already exists");
            }

            employee.FirstName = request.FirstName.Trim();
            employee.LastName = request.LastName.Trim();
            employee.Email = normalizedEmail;
            employee.Phone = request.Phone?.Trim();
            employee.PositionId = request.PositionId;
            employee.HireDate = request.HireDate.ToUniversalTime();
            employee.IsActive = request.IsActive;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(x => x.Id == id);

            if (employee is null)
            {
                throw new NotFoundException("Employee not found");
            }

            var hasUserAccount = await _context.UserAccounts
                .AnyAsync(x => x.EmployeeId == id);

            if (hasUserAccount)
            {
                throw new ConflictException("Employee has a user account and cannot be deleted");
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task ValidatePositionAsync(Guid positionId)
        {
            var positionExists = await _context.Positions
                .AnyAsync(x => x.Id == positionId);

            if (!positionExists)
            {
                throw new NotFoundException("Position not found");
            }
        }

        private static string NormalizeEmail(string email)
        {
            return email.Trim().ToLowerInvariant();
        }
    }
}
