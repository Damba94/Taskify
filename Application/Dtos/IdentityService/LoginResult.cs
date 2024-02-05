namespace Application.Dtos.IdentityService
{
    public class LoginResult
    {
        public string Jwt { get; init; } = null!;
        public string RefreshToken { get; init; } = null!;
    }
}
