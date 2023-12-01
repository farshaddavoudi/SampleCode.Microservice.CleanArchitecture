using AutoMapper;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.Common.Exceptions;
using SampleMicroserviceApp.Identity.Application.CQRS.Claim.Specifications;
using SampleMicroserviceApp.Identity.Domain.Entities.Application;
using SampleMicroserviceApp.Identity.Domain.Entities.Claim;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Claim.Commands.CreateOrUpdateClaim;

public class CreateOrUpdateClaimCommand : IRequest
{
    public int? Id { get; set; }

    public int ApplicationId { get; set; }

    public string? Key { get; set; }

    public string? Title { get; set; }
}

// Handler
public class CreateOrUpdateClaimCommandHandler : IRequestHandler<CreateOrUpdateClaimCommand>
{
    private readonly IMapper _mapper;
    private readonly IRepository<ClaimEntity> _claimRepository;
    private readonly IRepository<ApplicationEntity> _applicationRepository;

    #region ctor

    public CreateOrUpdateClaimCommandHandler(IMapper mapper, IRepository<ClaimEntity> claimRepository, IRepository<ApplicationEntity> applicationRepository)
    {
        _mapper = mapper;
        _claimRepository = claimRepository;
        _applicationRepository = applicationRepository;
    }

    #endregion

    public async Task Handle(CreateOrUpdateClaimCommand request, CancellationToken cancellationToken)
    {
        if (request.Id is null) //=Add
        {
            // Get the app
            var app = await _applicationRepository.GetByIdAsync(request.ApplicationId, cancellationToken);

            if (app is null)
                throw new BadRequestException("The application is invalid");

            request.Key = $"{app.Key}_{request.Key}";

            // Check the Key to be repetitive
            var isClaimWithSameKeyExists = await _claimRepository.AnyAsync(
                    new ClaimByKeySpec(request.Key), cancellationToken);

            if (isClaimWithSameKeyExists)
                throw new BusinessLogicException("The app key cannot be repetitive. A claim with same key already exists.");

            // Insert new Claim
            var claimToAdd = _mapper.Map<ClaimEntity>(request);

            await _claimRepository.AddAsync(claimToAdd, cancellationToken);
        }
        else //=Update
        {
            // TODO
        }
    }
}