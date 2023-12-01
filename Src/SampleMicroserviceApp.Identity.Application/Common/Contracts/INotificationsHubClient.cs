namespace SampleMicroserviceApp.Identity.Application.Common.Contracts;

/// <summary>
/// Contains the methods we can use from Client e.g. Blazor
/// </summary>
public interface INotificationsHubClient
{
    Task BroadcastNotification(string message);
}