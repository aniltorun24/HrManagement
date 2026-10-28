using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using HrManagement.Application.Common.Validation;

namespace HrManagement.Application.Departments
{
    public class CreateDepartmentRequest
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [NotEmptyGuid]
        public Guid CompanyId { get; set; }
    }
}
