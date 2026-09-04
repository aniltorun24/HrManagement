using HrManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagement.Application.Positions
{
    public class CreatePositionRequest
    {
        public string Title { get; set; }
        public Guid DepartmentId { get; set; }
        
    }
}
