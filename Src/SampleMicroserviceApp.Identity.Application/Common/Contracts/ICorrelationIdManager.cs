namespace SampleMicroserviceApp.Identity.Application.Common.Contracts;

public interface ICorrelationIdManager
{
    string Get();

    void Set(string correlationId);
}