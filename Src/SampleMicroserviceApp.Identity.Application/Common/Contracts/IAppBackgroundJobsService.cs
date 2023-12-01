namespace SampleMicroserviceApp.Identity.Application.Common.Contracts;

public interface IAppBackgroundJobsService
{
    void SyncUsersWithRahkaran();
}