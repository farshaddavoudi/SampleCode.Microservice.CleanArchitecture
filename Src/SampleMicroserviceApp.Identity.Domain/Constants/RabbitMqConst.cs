namespace SampleMicroserviceApp.Identity.Domain.Constants;

public static class RabbitMqConst
{
    public static class MessageRouteKey
    {
        public static string RahkaranUserEdited(string changedSubject) => $"user.rahkaran.edited.{changedSubject}";
    }

    public static class Exchange
    {
        public const string UserExchangeName = "user-exchange";
        public const string GlobalAlternateExchangeName = "alternate-exchange";
    }
}