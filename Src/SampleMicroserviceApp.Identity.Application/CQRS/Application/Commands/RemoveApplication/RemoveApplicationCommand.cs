using MediatR;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Application.Commands.RemoveApplication;

public record RemoveApplicationCommand(int ApplicationId) : IRequest;

// Handler
public class RemoveApplicationCommandHandler : IRequestHandler<RemoveApplicationCommand>
{
    public Task Handle(RemoveApplicationCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();

        // TODO: Remove Cache as well with MediatR Pub/Sub
    }
}