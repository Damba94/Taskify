namespace Application.Dtos.IdentityService
{
    public class UserLoginDto
    {
        public string Email { get; init; } = null!;
        public string Password { get; init; } = null!;
    }
}
