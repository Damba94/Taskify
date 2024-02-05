using Application.Dtos.IdentityService;
using Riok.Mapperly.Abstractions;

namespace Api.Dtos.AuthDtos
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; init; } = null!;
    }

    [Mapper]
    public static partial class RefreshTokenRequestMapper
    {
        public static RefreshTokenDto ToApplicationDto(this RefreshTokenRequest refreshTokenRequest, string email)
        {
            var mapped = ToApplicationDto(refreshTokenRequest);
            mapped.Email = email;
            return mapped;
        }

        private static partial RefreshTokenDto ToApplicationDto(this RefreshTokenRequest refreshTokenRequest);
    }
}
