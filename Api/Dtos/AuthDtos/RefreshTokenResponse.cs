using Application.Dtos.IdentityService;
using Riok.Mapperly.Abstractions;

namespace Api.Dtos.AuthDtos
{
    public class RefreshTokenResponse
    {
        public string Jwt { get; init; } = null!;
        public string RefreshToken { get; init; } = null!;
    }

    [Mapper]
    public static partial class RefreshTokenResponseMapper
    {
        public static partial RefreshTokenResponse ToDto(this RefreshTokenResult refreshTokenResult);
    }
}
