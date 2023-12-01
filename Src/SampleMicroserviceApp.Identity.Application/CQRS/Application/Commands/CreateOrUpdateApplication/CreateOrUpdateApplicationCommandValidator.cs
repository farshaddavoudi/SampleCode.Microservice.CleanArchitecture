using FluentValidation;
using SampleMicroserviceApp.Identity.Domain.Entities.Application;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Application.Commands.CreateOrUpdateApplication;

public class CreateOrUpdateApplicationCommandValidator : AbstractValidator<ApplicationEntity>
{
    public CreateOrUpdateApplicationCommandValidator()
    {
        RuleFor(x => x.Key)
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(x => x.Title)
            .NotEmpty();

        // TODO: More validations here
    }
}