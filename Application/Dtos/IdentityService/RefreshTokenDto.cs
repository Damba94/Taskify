namespace Application.Dtos.IdentityService
{
    public class RefreshTokenDto
    {
        public string Email { get; set; } = null!;
        public string RefreshToken { get; init; } = null!;
    }
}
