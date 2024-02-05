using Application.Dtos.IdentityService;
using Riok.Mapperly.Abstractions;

namespace Api.Dtos.AuthDtos
{
    public class RegisterResponse
    {
        public string Jwt { get; init; } = null!;
        public string RefreshToken { get; init; } = null!;
    }

    [Mapper]
    public static partial class RegisterResponseMapper
    {
        public static partial RegisterResponse ToDto(this RegisterResult registerRequest);
    }
}
