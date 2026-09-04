using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagement.Application.Departments
{
    public class CreateDepartmentRequest
    {
        public string Name { get; set; }
        public Guid CompanyId { get; set; }
    }
}
