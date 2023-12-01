namespace SampleMicroserviceApp.Identity.Domain.ConfigurationSettings;

public class AppSettings
{
    public ConnStrSettings? ConnStrSettings { get; set; }

    public UrlSettings? UrlSettings { get; set; }

    public AuthSettings? AuthSettings { get; set; }

    public SeqSettings? SeqSettings { get; set; }

    public MongoDbSettings? MongoDbSettings { get; set; }

    public RedisSettings? RedisSettings { get; set; }

    public RabbitMqSettings? RabbitMqSettings { get; set; }

    /// <summary>
    /// Is development environment, if false it is production
    /// </summary>
    public bool IsDevelopment { get; set; }

    public bool IsProduction => IsDevelopment is false;
}