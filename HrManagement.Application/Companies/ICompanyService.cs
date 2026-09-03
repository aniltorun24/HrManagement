using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagement.Application.Companies
{
    public interface ICompanyService 
    {
        Task<Guid> CreateAsync(CreateCompanyRequest request);

        Task<List<CompanyResponse>> GetAllAsync();

        Task<CompanyResponse> GetByIdAsync(Guid id);

        Task<bool> UpdateAsync(Guid id, UpdateCompanyAsync request);

        Task<bool> DeleteAsync(Guid id);
    }
}
