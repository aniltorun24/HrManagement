using HrManagement.Domain.Enums;

namespace HrManagement.Application.UserAccounts
{
    public class UserAccountResponse
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public bool EmailVerified { get; set; }
        public UserAccountStatus Status { get; set; }
    }
}
