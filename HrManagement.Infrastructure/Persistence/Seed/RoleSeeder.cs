using HrManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagement.Infrastructure.Persistence.Seed
{
    public class RoleSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var roleNames = new[]
        {
            "Admin",
            "HRManager",
            "HRSpecialist",
            "Employee"
        };

            var existingRoleNames = await context.Roles
                .Where(x => roleNames.Contains(x.Name))
                .Select(x => x.Name)
                .ToListAsync();

            var missingRoles = roleNames
                .Where(roleName => !existingRoleNames.Contains(roleName))
                .Select(roleName => new Role
                {
                    Id = Guid.NewGuid(),
                    Name = roleName,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                })
                .ToList();

            if (missingRoles.Count == 0)
                return;

            await context.Roles.AddRangeAsync(missingRoles);
            await context.SaveChangesAsync();
        }
    }
}
