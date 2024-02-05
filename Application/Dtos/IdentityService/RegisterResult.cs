namespace Application.Dtos.IdentityService
{
    public class RegisterResult
    {
        public string Jwt { get; init; } = null!;
        public string RefreshToken { get; init; } = null!;
    }
}
