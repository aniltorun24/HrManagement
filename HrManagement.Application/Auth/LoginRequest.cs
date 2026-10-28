using System.ComponentModel.DataAnnotations;

namespace HrManagement.Application.Auth
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        [StringLength(250)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(128, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;
    }
}
