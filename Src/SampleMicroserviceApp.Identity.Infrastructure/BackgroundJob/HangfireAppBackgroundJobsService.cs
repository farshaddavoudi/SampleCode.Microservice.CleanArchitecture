using Hangfire;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.ServicesContracts;
using SampleMicroserviceApp.Identity.Domain.Constants;

namespace SampleMicroserviceApp.Identity.Infrastructure.BackgroundJob;

public class HangfireAppBackgroundJobsService(IRecurringJobManager hangfireRecurringJobManager, ISyncUsersService syncUsersService)
    : IAppBackgroundJobsService
{
    public void SyncUsersWithRahkaran()
    {
        hangfireRecurringJobManager.AddOrUpdate(HangfireConst.JobId.SyncUsersWithRahkaran,
            HangfireConst.Queue.DefaultQueue,
            () => syncUsersService.ExecuteAsync(CancellationToken.None),
            HangfireConst.CronExpression.EveryXMinutes(5));
    }
}