using HrManagement.Application.Positions;
using HrManagement.Application.Common.Exceptions;
using HrManagement.Domain.Entities;
using HrManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HrManagement.Infrastructure.Services
{
    public class PositionService : IPositionService
    {
        private readonly ApplicationDbContext _context;

        public PositionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(CreatePositionRequest request)
        {
            var departmentExists = await _context.Departments
                .AnyAsync(x => x.Id == request.DepartmentId);

            if (!departmentExists)
            {
                throw new NotFoundException("Department not found");
            }

            var position = new Position
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                DepartmentId = request.DepartmentId,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();

            return position.Id;
        }

        public async Task<List<PositionResponse>> GetAllAsync()
        {
            return await _context.Positions
                .AsNoTracking()
                .Select(x => new PositionResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    DepartmentId = x.DepartmentId,
                    CreatedAt = x.CreatedAt,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<PositionResponse> GetByIdAsync(Guid id)
        {
            var position = await _context.Positions
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new PositionResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    DepartmentId = x.DepartmentId,
                    CreatedAt = x.CreatedAt,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();

            if (position is null)
            {
                throw new NotFoundException("Position not found");
            }

            return position;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdatePositionRequest request)
        {
            var position = await _context.Positions
                .FirstOrDefaultAsync(x => x.Id == id);

            if (position is null)
            {
                throw new NotFoundException("Position not found");
            }

            position.Title = request.Title;
            position.IsActive = request.IsActive;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var position = await _context.Positions
                .FirstOrDefaultAsync(x => x.Id == id);

            if (position is null)
            {
                throw new NotFoundException("Position not found");
            }

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
