using Application.Dtos.IdentityService;
using Riok.Mapperly.Abstractions;

namespace Api.Dtos.AuthDtos
{
    public class LoginResponse
    {
        public string Jwt { get; init; } = null!;
        public string RefreshToken { get; init; } = null!;
    }

    [Mapper]
    public static partial class LoginResponseMapper
    {
        public static partial LoginResponse ToDto(this LoginResult loginRequest);
    }
}
