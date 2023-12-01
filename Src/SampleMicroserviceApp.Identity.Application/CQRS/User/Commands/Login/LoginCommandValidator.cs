using FluentValidation;

namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty();

        RuleFor(x => x.Password).NotEmpty();
    }
}