using Application.Dtos.IdentityService;
using Riok.Mapperly.Abstractions;

namespace Api.Dtos.AuthDtos
{
    public class LoginRequest
    {
        public string Email { get; init; } = null!;
        public string Password { get; init; } = null!;
    }

    [Mapper]
    public static partial class LoginRequestMapper
    {
        public static partial UserLoginDto ToApplicationDto(this LoginRequest loginRequest);
    }
}
