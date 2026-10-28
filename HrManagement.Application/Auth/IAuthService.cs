namespace HrManagement.Application.Auth
{
    public interface IAuthService
    {
        Task<AuthenticatedUserResponse> AuthenticateAsync(LoginRequest request);
    }
}
