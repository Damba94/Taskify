using System.Security.Claims;

namespace Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetEmail(this ClaimsPrincipal principal) =>
            principal.FindFirstValue(ClaimTypes.Email)
            ?? throw new ArgumentException(
                $"Request does not include the ´{ClaimTypes.Email}´ claim in the bearer token.");
    }
}
