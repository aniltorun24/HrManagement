using HrManagement.Application.Auth;
using HrManagement.Application.Common.Exceptions;
using HrManagement.Application.UserAccounts;
using HrManagement.Domain.Entities;
using HrManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HrManagement.Infrastructure.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UserAccountService(ApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> CreateAsync(CreateUserAccountRequest request)
        {
            await ValidateEmployeeAsync(request.EmployeeId);
            await ValidateRoleAsync(request.RoleId);

            var accountExists = await _context.UserAccounts
                .AnyAsync(x => x.EmployeeId == request.EmployeeId);

            if (accountExists)
            {
                throw new ConflictException("Employee already has a user account");
            }

            var now = DateTime.UtcNow;
            var userAccount = new UserAccount
            {
                Id = Guid.NewGuid(),
                EmployeeId = request.EmployeeId,
                PasswordHash = _passwordHasher.Hash(request.Password),
                CreatedAt = now,
                LastLoginAt = null,
                EmailVerified = request.EmailVerified,
                Status = request.Status,
                RoleId = request.RoleId
            };

            await _context.UserAccounts.AddAsync(userAccount);
            await _context.SaveChangesAsync();

            return userAccount.Id;
        }

        public async Task<List<UserAccountResponse>> GetAllAsync()
        {
            return await _context.UserAccounts
                .AsNoTracking()
                .Select(x => new UserAccountResponse
                {
                    Id = x.Id,
                    EmployeeId = x.EmployeeId,
                    RoleId = x.RoleId,
                    CreatedAt = x.CreatedAt,
                    LastLoginAt = x.LastLoginAt,
                    EmailVerified = x.EmailVerified,
                    Status = x.Status
                })
                .ToListAsync();
        }

        public async Task<UserAccountResponse> GetByIdAsync(Guid id)
        {
            var userAccount = await _context.UserAccounts
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new UserAccountResponse
                {
                    Id = x.Id,
                    EmployeeId = x.EmployeeId,
                    RoleId = x.RoleId,
                    CreatedAt = x.CreatedAt,
                    LastLoginAt = x.LastLoginAt,
                    EmailVerified = x.EmailVerified,
                    Status = x.Status
                })
                .FirstOrDefaultAsync();

            if (userAccount is null)
            {
                throw new NotFoundException("User account not found");
            }

            return userAccount;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateUserAccountRequest request)
        {
            var userAccount = await _context.UserAccounts
                .FirstOrDefaultAsync(x => x.Id == id);

            if (userAccount is null)
            {
                throw new NotFoundException("User account not found");
            }

            await ValidateRoleAsync(request.RoleId);

            userAccount.RoleId = request.RoleId;
            userAccount.EmailVerified = request.EmailVerified;
            userAccount.Status = request.Status;

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                userAccount.PasswordHash = _passwordHasher.Hash(request.Password);
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var userAccount = await _context.UserAccounts
                .FirstOrDefaultAsync(x => x.Id == id);

            if (userAccount is null)
            {
                throw new NotFoundException("User account not found");
            }

            _context.UserAccounts.Remove(userAccount);
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task ValidateEmployeeAsync(Guid employeeId)
        {
            var employeeExists = await _context.Employees
                .AnyAsync(x => x.Id == employeeId);

            if (!employeeExists)
            {
                throw new NotFoundException("Employee not found");
            }
        }

        private async Task ValidateRoleAsync(Guid roleId)
        {
            var roleExists = await _context.Roles
                .AnyAsync(x => x.Id == roleId);

            if (!roleExists)
            {
                throw new NotFoundException("Role not found");
            }
        }

    }
}
