using FluentValidation;
using SampleMicroserviceApp.Identity.Domain.Entities.Role;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.CreateOrUpdateRole;

public class CreateOrUpdateRoleCommandValidator : AbstractValidator<RoleEntity>
{
    public CreateOrUpdateRoleCommandValidator()
    {
        RuleFor(x => x.Key)
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(x => x.Title)
            .NotEmpty();

        // TODO: More validations here
    }
}