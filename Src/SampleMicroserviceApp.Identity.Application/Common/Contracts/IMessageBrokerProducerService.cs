namespace SampleMicroserviceApp.Identity.Application.Common.Contracts;

public interface IMessageBrokerProducerService
{
    void PublishUserEdited(int userId, string messageRouteKey);
}