namespace Application.Dtos.IdentityService
{
    public class RefreshTokenResult
    {
        public string Jwt { get; init; } = null!;
        public string RefreshToken { get; init; } = null!;
    }
}
