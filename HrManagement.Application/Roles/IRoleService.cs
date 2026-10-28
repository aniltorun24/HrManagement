namespace HrManagement.Application.Roles
{
    public interface IRoleService
    {
        Task<Guid> CreateAsync(CreateRoleRequest request);
        Task<List<RoleResponse>> GetAllAsync();
        Task<RoleResponse> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Guid id, UpdateRoleRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
