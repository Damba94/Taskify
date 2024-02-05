using Api.Dtos.AuthDtos;
using Api.Extensions;
using Application.Enums.IdentityService;
using Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IValidator<RegisterRequest> _registerRequestValidator;
        private readonly IValidator<LoginRequest> _loginRequestValidator;
        private readonly IValidator<RefreshTokenRequest> _refreshTokenRequestValidator;
        private readonly IIdentityService _identityService;

        public AuthController(
            IValidator<RegisterRequest> registerRequestValidator,
            IIdentityService identityService,
            IValidator<LoginRequest> loginRequestValidator,
            IValidator<RefreshTokenRequest> refreshTokenRequestValidator)
        {
            _registerRequestValidator = registerRequestValidator;
            _identityService = identityService;
            _loginRequestValidator = loginRequestValidator;
            _refreshTokenRequestValidator = refreshTokenRequestValidator;
        }

        [HttpPost(Routes.Auth.Register)]
        public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterRequest registerRequest)
        {
            await _registerRequestValidator
                .ValidateAndThrowAsync(registerRequest);

            var (status, value) = await _identityService
                .Register(registerRequest.ToApplicationDto());

            if (status is not RegistrationStatus.Registered)
                return BadRequest();

            return Ok(value!.ToDto());
        }

        [HttpPost(Routes.Auth.Login)]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            await _loginRequestValidator
                .ValidateAndThrowAsync(loginRequest);

            var (status, value) = await _identityService
                .Login(loginRequest.ToApplicationDto());

            if (status is not LoginStatus.Success)
                return BadRequest();

            return Ok(value!.ToDto());
        }

        [HttpPost(Routes.Auth.RefreshToken)]
        public async Task<ActionResult<RefreshTokenResponse>> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            await _refreshTokenRequestValidator
                .ValidateAndThrowAsync(refreshTokenRequest);

            var mappedRequest = refreshTokenRequest.ToApplicationDto(User.GetEmail());

            var (status, value) = await _identityService
                .RefreshToken(mappedRequest);

            if (status is not RefreshTokenStatus.Valid)
                return BadRequest();

            return Ok(value!.ToDto());
        }
    }
}
