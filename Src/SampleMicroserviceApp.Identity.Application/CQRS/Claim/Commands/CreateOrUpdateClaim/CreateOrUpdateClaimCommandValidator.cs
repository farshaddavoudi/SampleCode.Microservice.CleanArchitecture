using FluentValidation;
using SampleMicroserviceApp.Identity.Domain.Entities.Claim;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Claim.Commands.CreateOrUpdateClaim;

public class CreateOrUpdateClaimCommandValidator : AbstractValidator<ClaimEntity>
{
    public CreateOrUpdateClaimCommandValidator()
    {
        RuleFor(x => x.Key)
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(x => x.Title)
            .NotEmpty();

        // TODO: More validations here
    }
}