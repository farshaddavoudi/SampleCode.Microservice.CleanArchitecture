using Hangfire;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;
using SampleMicroserviceApp.Identity.Application.ServicesContracts;
using SampleMicroserviceApp.Identity.Domain.Constants;

namespace SampleMicroserviceApp.Identity.Infrastructure.BackgroundJob;

public class HangfireAppBackgroundJobsService : IAppBackgroundJobsService
{
    private readonly IRecurringJobManager _hangfireRecurringJobManager;
    private readonly ISyncUsersService _syncUsersService;

    #region ctor
    public HangfireAppBackgroundJobsService(IRecurringJobManager hangfireRecurringJobManager, ISyncUsersService syncUsersService)
    {
        _hangfireRecurringJobManager = hangfireRecurringJobManager;
        _syncUsersService = syncUsersService;
    }
    #endregion

    public void SyncUsersWithRahkaran()
    {
        _hangfireRecurringJobManager.AddOrUpdate(HangfireConst.JobId.SyncUsersWithRahkaran,
            HangfireConst.Queue.DefaultQueue,
            () => _syncUsersService.ExecuteAsync(CancellationToken.None),
            HangfireConst.CronExpression.EveryXMinutes(5));
    }
}