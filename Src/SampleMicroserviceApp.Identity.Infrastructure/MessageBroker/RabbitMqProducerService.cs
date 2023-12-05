using RabbitMQ.Client;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Domain.Constants;
using SampleMicroserviceApp.Identity.Domain.Shared;
using System.Text;

namespace SampleMicroserviceApp.Identity.Infrastructure.MessageBroker;

public class RabbitMqProducerService(AppSettings appSettings) : IMessageBrokerProducerService
{
    public void PublishUserEdited(int userId, string messageRouteKey)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(appSettings.RabbitMqSettings!.Uri),
            ClientProvidedName = AppMetadataConst.SolutionName
        };

        using var connection = factory.CreateConnection();

        using var channel = connection.CreateModel();

        var exchangeArgs = new Dictionary<string, object>
        {
            { "alternate-exchange", RabbitMqConst.Exchange.GlobalAlternateExchangeName }
        };

        channel.ExchangeDeclare(RabbitMqConst.Exchange.UserExchangeName, ExchangeType.Topic, arguments: exchangeArgs);

        byte[] messageBody = Encoding.UTF8.GetBytes(userId.ToString());

        channel.BasicPublish(RabbitMqConst.Exchange.UserExchangeName, messageRouteKey, body: messageBody);
    }
}