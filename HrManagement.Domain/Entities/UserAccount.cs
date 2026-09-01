using HrManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HrManagement.Domain.Entities
{
    public class UserAccount
    {
        public Guid Id { get; set; }
        public Guid EmpoyeeId { get; set; }
        public string? PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastLoginAt { get; set; }
        public bool EmailVerified { get; set; }
        public UserAccountStatus Status { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
