using HrManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HrManagement.Application.Common.Validation;
using System.ComponentModel.DataAnnotations;

namespace HrManagement.Application.Positions
{
    public class CreatePositionRequest
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;

        [NotEmptyGuid]
        public Guid DepartmentId { get; set; }
        
    }
}
