using RabbitMQ.Client;
using System.Text;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;
using SampleMicroserviceApp.Identity.Domain.Constants;
using SampleMicroserviceApp.Identity.Domain.Shared;

namespace SampleMicroserviceApp.Identity.Infrastructure.MessageBroker;

public class RabbitMqProducerService : IMessageBrokerProducerService
{
    private readonly AppSettings _appSettings;

    #region ctor

    public RabbitMqProducerService(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }

    #endregion

    public void PublishUserEdited(int userId, string messageRouteKey)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(_appSettings.RabbitMqSettings!.Uri),
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