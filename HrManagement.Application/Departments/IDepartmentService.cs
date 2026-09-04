using HrManagement.Application.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagement.Application.Departments
{
    public interface IDepartmentService
    {
        Task<Guid> CreateAsync(CreateDepartmentRequest request);

        Task<List<DepartmentResponse>> GetAllAsync();

        Task<DepartmentResponse> GetByIdAsync(Guid id);

        Task<bool> UpdateAsync(Guid id, UpdateDepartmentRequest request);

        Task<bool> DeleteAsync(Guid id);

    }
}
