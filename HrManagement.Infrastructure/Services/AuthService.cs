using HrManagement.Application.Auth;
using HrManagement.Application.Common.Exceptions;
using HrManagement.Domain.Enums;
using HrManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HrManagement.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(ApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthenticatedUserResponse> AuthenticateAsync(LoginRequest request)
        {
            var normalizedEmail = request.Email.Trim().ToLowerInvariant();
            var userAccount = await _context.UserAccounts
                .Include(x => x.Employee)
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Employee.Email.ToLower() == normalizedEmail);

            if (userAccount?.PasswordHash is null ||
                !_passwordHasher.Verify(request.Password, userAccount.PasswordHash))
            {
                throw new UnauthorizedException("Invalid email or password");
            }

            if (userAccount.Status != UserAccountStatus.Active ||
                !userAccount.EmailVerified ||
                !userAccount.Employee.IsActive ||
                !userAccount.Role.IsActive)
            {
                throw new UnauthorizedException("User account is not active");
            }

            userAccount.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return new AuthenticatedUserResponse
            {
                UserAccountId = userAccount.Id,
                EmployeeId = userAccount.EmployeeId,
                Email = userAccount.Employee.Email,
                Role = userAccount.Role.Name
            };
        }
    }
}
