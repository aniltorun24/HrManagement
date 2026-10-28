using System.ComponentModel.DataAnnotations;

namespace HrManagement.Application.Roles
{
    public class UpdateRoleRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
