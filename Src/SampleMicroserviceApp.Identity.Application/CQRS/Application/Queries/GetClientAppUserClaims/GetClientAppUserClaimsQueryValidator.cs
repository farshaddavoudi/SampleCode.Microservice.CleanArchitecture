using FluentValidation;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Application.Queries.GetClientAppUserClaims;

public class GetClientAppUserClaimsQueryValidator : AbstractValidator<GetClientAppUserAllClaimsQuery>
{
    public GetClientAppUserClaimsQueryValidator()
    {
        RuleFor(x => x.ClientAppKey)
            .NotEmpty().WithMessage($"{nameof(GetClientAppUserAllClaimsQuery.ClientAppKey)} is missing");
    }
}