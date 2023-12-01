using MediatR;
using SampleMicroserviceApp.Identity.Domain.Entities.Application;

namespace SampleMicroserviceApp.Identity.Application.CQRS.Application.Commands.CreateOrUpdateApplication.Events;

public record ApplicationCreatedEvent(ApplicationEntity App) : INotification;