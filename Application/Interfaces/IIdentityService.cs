using Application.Dtos.IdentityService;
using Application.Enums.IdentityService;

namespace Application.Interfaces
{
    public interface IIdentityService
    {
        Task<(LoginStatus Status, LoginResult? Value)> Login(UserLoginDto userLoginData);
        Task<(RefreshTokenStatus Status, RefreshTokenResult? Value)> RefreshToken(RefreshTokenDto refreshTokenData);
        Task<(RegistrationStatus Status, RegisterResult? Value)> Register(UserRegisterDto userRegisterData);
    }
}
