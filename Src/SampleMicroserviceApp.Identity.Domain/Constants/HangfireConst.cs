namespace SampleMicroserviceApp.Identity.Domain.Constants;

public static class HangfireConst
{
    public const string Path = "/hangfire";

    public class JobId
    {
        public const string SyncUsersWithRahkaran = "SYNC USERS WITH RAHKARAN";
    }

    public class Queue
    {
        public const string DefaultQueue = "default-queue";
    }

    public static class CronExpression
    {
        public static string EveryXMinutes(int x) => $"0 */{x} * ? * *";
    }
}