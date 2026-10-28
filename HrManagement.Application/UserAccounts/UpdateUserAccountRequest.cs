using HrManagement.Domain.Enums;
using HrManagement.Application.Common.Validation;
using System.ComponentModel.DataAnnotations;

namespace HrManagement.Application.UserAccounts
{
    public class UpdateUserAccountRequest
    {
        [NotEmptyGuid]
        public Guid RoleId { get; set; }

        [StringLength(128, MinimumLength = 8)]
        public string? Password { get; set; }
        public bool EmailVerified { get; set; }

        [EnumDataType(typeof(UserAccountStatus))]
        public UserAccountStatus Status { get; set; }
    }
}
