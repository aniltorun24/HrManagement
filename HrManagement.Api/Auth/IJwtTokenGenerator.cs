using HrManagement.Application.Auth;

namespace HrManagement.Api.Auth
{
    public interface IJwtTokenGenerator
    {
        LoginResponse Generate(AuthenticatedUserResponse user);
    }
}
