using FluentValidation;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Role.Commands.ChangeRoleClaims;

public class ChangeRoleClaimsCommandValidator : AbstractValidator<ChangeRoleClaimsCommand>
{
    public ChangeRoleClaimsCommandValidator()
    {
        //RuleFor(x => x.RoleId)
        //    .NotEqual(nameof(Administrator)).WithMessage("Administrator user permissions is not editable");
    }
}