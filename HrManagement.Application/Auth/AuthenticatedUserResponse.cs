namespace HrManagement.Application.Auth
{
    public class AuthenticatedUserResponse
    {
        public Guid UserAccountId { get; set; }
        public Guid EmployeeId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
