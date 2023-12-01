using FluentValidation;

namespace SampleMicroserviceApp.Identity.Application.CQRS.User.Commands.GenerateNewToken;

public class GenerateNewTokenCommandValidator : AbstractValidator<GenerateNewTokenCommand>
{
    public GenerateNewTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}