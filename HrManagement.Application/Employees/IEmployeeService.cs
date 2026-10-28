namespace HrManagement.Application.Employees
{
    public interface IEmployeeService
    {
        Task<Guid> CreateAsync(CreateEmployeeRequest request);
        Task<List<EmployeeResponse>> GetAllAsync();
        Task<EmployeeResponse> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Guid id, UpdateEmployeeRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
