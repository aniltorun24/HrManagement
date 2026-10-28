using System.ComponentModel.DataAnnotations;

namespace HrManagement.Application.Roles
{
    public class CreateRoleRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;
    }
}
