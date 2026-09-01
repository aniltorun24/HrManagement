using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagement.Domain.Entities
{
    public class Position
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Employee> Employees { get; set; }
        
    }
}
