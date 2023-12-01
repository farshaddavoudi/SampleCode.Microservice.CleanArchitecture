namespace SampleMicroserviceApp.Identity.Application.ServicesContracts;

public interface ISyncUsersService
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}