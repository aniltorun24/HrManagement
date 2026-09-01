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
    }
}
