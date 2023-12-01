using AutoMapper;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.Common.Exceptions;
using SampleMicroserviceApp.Identity.Application.CQRS.Application.Commands.CreateOrUpdateApplication.Events;
using SampleMicroserviceApp.Identity.Application.CQRS.Application.Specifications;
using SampleMicroserviceApp.Identity.Domain.Entities.Application;
using SampleMicroserviceApp.Identity.Domain.Enums.Application;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Application.Commands.CreateOrUpdateApplication;

public class CreateOrUpdateApplicationCommand : IRequest
{
    public int? Id { get; set; }

    public string? Key { get; set; }

    public string? Title { get; set; }

    public AppType AppType { get; set; }

    public bool IsActive { get; set; }

    public string? IconUrl { get; set; }

    public string? BaseAddress { get; set; }

    public List<int>? RelatedApps { get; set; }

    public string? Description { get; set; }
}

// Handler
public class CreateOrUpdateApplicationCommandHandler : IRequestHandler<CreateOrUpdateApplicationCommand>
{
    private readonly IMapper _mapper;
    private readonly IRepository<ApplicationEntity> _applicationRepository;
    private readonly IMediator _mediator;

    #region ctor

    public CreateOrUpdateApplicationCommandHandler(IMapper mapper, IRepository<ApplicationEntity> applicationRepository, IMediator mediator)
    {
        _mapper = mapper;
        _applicationRepository = applicationRepository;
        _mediator = mediator;
    }

    #endregion

    public async Task Handle(CreateOrUpdateApplicationCommand request, CancellationToken cancellationToken)
    {
        if (request.Id is null) //=Add
        {
            if (request.Key is null)
                throw new BadRequestException("App key is missing");

            // Check the Key to be repetitive
            var isAppWithSameKeyExists = await _applicationRepository.AnyAsync(
                    new ApplicationByKeySpec(request.Key), cancellationToken);

            if (isAppWithSameKeyExists)
                throw new BusinessLogicException("The app key cannot be repetitive. An app with same key already exists.");

            // Insert new application
            var applicationToAdd = _mapper.Map<ApplicationEntity>(request);

            await _applicationRepository.AddAsync(applicationToAdd, cancellationToken);

            // Publish created event
            await _mediator.Publish(new ApplicationCreatedEvent(applicationToAdd), cancellationToken);
        }
        else //=Update
        {
            // TODO : Manage Cache As Well
        }
    }
}