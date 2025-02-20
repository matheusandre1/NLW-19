using FluentValidation;
using TechLibrary.Communication.Request;

namespace TechLibrary.Application.UseCases.Users
{
    public class RegisterUserValidator : AbstractValidator<RequestUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(x=> x.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Email is not valid");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required");

            When(x => string.IsNullOrEmpty(x.Password) == false, () =>
            {
                RuleFor(x => x.Password)
                    .MinimumLength(6)
                    .WithMessage("Password must be at least 6 characters");
            });

        }
    }
}
