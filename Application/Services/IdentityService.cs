using Application.Dtos.IdentityService;
using Application.Enums.IdentityService;
using Application.Interfaces;
using Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IConfiguration _configuration;
        private readonly IPasswordService _passwordService;
        private readonly int _iterations;
        private readonly int _hashLength;
        private readonly int _saltLength;
        private readonly IUserService _userService;
        private readonly int _refreshTokenValidDays;
        private readonly int _refreshTokenLength;

        public IdentityService(
            IConfiguration configuration,
            IPasswordService passwordService,
            IUserService userService)
        {
            _configuration = configuration;
            _passwordService = passwordService;
            _iterations = _configuration.GetValue<int>("Security:PasswordHashIterations");
            _hashLength = _configuration.GetValue<int>("Security:HashLengthBytes");
            _saltLength = _configuration.GetValue<int>("Security:SaltLengthBytes");
            _userService = userService;
            _refreshTokenValidDays = _configuration.GetValue<int>("Identity:RefreshTokenValidDays");
            _refreshTokenLength = _configuration.GetValue<int>("Identity:RefreshTokenLength");
        }

        public async Task<(RegistrationStatus Status, RegisterResult? Value)> Register(
            UserRegisterDto userRegisterData)
        {
            if (await _userService.AnyUserByEmail(userRegisterData.Email))
                return (RegistrationStatus.EmailInUseError, null);

            var (passwordHash, salt) = _passwordService.GeneratePassword(
                userRegisterData.Password,
                _iterations,
                _hashLength,
                _saltLength);

            var refreshToken = GenerateRefreshToken();

            var user = new User
            {
                Email = userRegisterData.Email.ToLower(),
                FirstName = userRegisterData.FirstName,
                LastName = userRegisterData.LastName,
                Password = passwordHash,
                Salt = salt,
                RefreshToken = refreshToken,
                RefreshTokenCreated = DateTime.UtcNow,
            };

            try
            {
                await _userService.AddUser(user);
            }
            catch (Exception)
            {
                return (RegistrationStatus.UnhandledError, null);
            }

            var Jwt = GenerateJwt(
                user.Id,
                user.FullName,
                user.Email);

            return (
                RegistrationStatus.Registered,
                new RegisterResult
                {
                    Jwt = Jwt,
                    RefreshToken = refreshToken,
                });
        }

        public async Task<(LoginStatus Status, LoginResult? Value)> Login(
            UserLoginDto userLoginData)
        {
            var user = await _userService.GetUserByEmail(userLoginData.Email);

            if (user is null)
                return (LoginStatus.UnknownEmailError, null);

            var passwordComparasionResult = _passwordService.ComparePassword(
                userLoginData.Password,
                user.Password,
                _iterations,
                _hashLength,
                user.Salt);

            if (passwordComparasionResult is false)
                return (LoginStatus.InvalidPasswordError, null);

            var Jwt = GenerateJwt(
                user.Id,
                user.FullName,
                user.Email);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenCreated = DateTime.UtcNow;

            await _userService.UpdateUser(user);

            return (
                LoginStatus.Success,
                new LoginResult
                {
                    Jwt = Jwt,
                    RefreshToken = refreshToken,
                });
        }

        public async Task<(RefreshTokenStatus Status, RefreshTokenResult? Value)> RefreshToken(RefreshTokenDto refreshTokenData)
        {
            var user = await _userService.GetUserByEmail(refreshTokenData.Email);

            if (user is null)
                return (RefreshTokenStatus.NoEmail, null);

            if (user.RefreshTokenCreated.AddDays(_refreshTokenValidDays) < DateTime.UtcNow)
                return (RefreshTokenStatus.Expired, null);

            var userRefreshTokenBytes = Encoding.UTF8.GetBytes(user.RefreshToken);
            var dataRefreshTokenBytes = Encoding.UTF8.GetBytes(refreshTokenData.RefreshToken);

            if (!CryptographicOperations.FixedTimeEquals(userRefreshTokenBytes, dataRefreshTokenBytes))
                return (RefreshTokenStatus.Invalid, null);

            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenCreated = DateTime.UtcNow;

            await _userService.UpdateUser(user);

            return (RefreshTokenStatus.Valid, new RefreshTokenResult
            {
                RefreshToken = refreshToken,
                Jwt = GenerateJwt(user.Id, user.FullName, user.Email),
            });

        }

        private string GenerateJwt(
            int userId,
            string fullName,
            string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenSecret = _configuration.GetValue<string>("Identity:TokenSecret")!;

            var key = Encoding.UTF8.GetBytes(tokenSecret);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new(ClaimTypes.Name, fullName),
                new(ClaimTypes.Email, email),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Identity:JwtLifetimeMinutes")),
                Issuer = _configuration.GetValue<string>("Identity:Issuer")!,
                Audience = _configuration.GetValue<string>("Identity:Audience")!,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var Jwt = tokenHandler.WriteToken(token);

            return Jwt;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = RandomNumberGenerator.GetBytes(_refreshTokenLength);

            return Convert.ToBase64String(randomNumber);
        }

    }
}
