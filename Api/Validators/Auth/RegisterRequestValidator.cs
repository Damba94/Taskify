using Api.Dtos.AuthDtos;
using FluentValidation;

namespace Api.Validators.Auth
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(r => r.Password)
                .NotEmpty()
                .NotNull();

            RuleFor(r => r.FirstName)
                .NotEmpty()
                .NotNull();

            RuleFor(r => r.LastName)
                .NotEmpty()
                .NotNull();
        }
    }
}
