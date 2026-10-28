using HrManagement.Application.Roles;
using HrManagement.Application.Common.Exceptions;
using HrManagement.Domain.Entities;
using HrManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HrManagement.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;

        public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(CreateRoleRequest request)
        {
            var normalizedName = request.Name.Trim();
            var normalizedNameLower = normalizedName.ToLowerInvariant();
            var roleExists = await _context.Roles
                .AnyAsync(x => x.Name.ToLower() == normalizedNameLower);

            if (roleExists)
            {
                throw new ConflictException("Role name already exists");
            }

            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = normalizedName,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            return role.Id;
        }

        public async Task<List<RoleResponse>> GetAllAsync()
        {
            return await _context.Roles
                .AsNoTracking()
                .Select(x => new RoleResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<RoleResponse> GetByIdAsync(Guid id)
        {
            var role = await _context.Roles
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new RoleResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();

            if (role is null)
            {
                throw new NotFoundException("Role not found");
            }

            return role;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateRoleRequest request)
        {
            var role = await _context.Roles
                .FirstOrDefaultAsync(x => x.Id == id);

            if (role is null)
            {
                throw new NotFoundException("Role not found");
            }

            var normalizedName = request.Name.Trim();
            var normalizedNameLower = normalizedName.ToLowerInvariant();
            var roleExists = await _context.Roles
                .AnyAsync(x => x.Id != id && x.Name.ToLower() == normalizedNameLower);

            if (roleExists)
            {
                throw new ConflictException("Role name already exists");
            }

            role.Name = normalizedName;
            role.IsActive = request.IsActive;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var role = await _context.Roles
                .FirstOrDefaultAsync(x => x.Id == id);

            if (role is null)
            {
                throw new NotFoundException("Role not found");
            }

            var roleIsUsed = await _context.UserAccounts
                .AnyAsync(x => x.RoleId == id);

            if (roleIsUsed)
            {
                throw new ConflictException("Role is assigned to a user account and cannot be deleted");
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
