using System.ComponentModel.DataAnnotations;
using HrManagement.Application.Common.Validation;

namespace HrManagement.Application.Employees
{
    public class UpdateEmployeeRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(250)]
        public string Email { get; set; } = string.Empty;

        [StringLength(30)]
        public string? Phone { get; set; }

        [NotEmptyGuid]
        public Guid PositionId { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
    }
}
