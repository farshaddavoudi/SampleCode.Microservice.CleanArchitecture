using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SampleMicroserviceApp.Identity.Application.Common.Contracts;

namespace SampleMicroserviceApp.Identity.Application.Common.SignalRHubs;

[Authorize]
public class NotificationsHub : Hub<INotificationsHubClient>
{
    public override async Task OnConnectedAsync()
    {
        var msg = $"{Context.ConnectionId} joined";

        await base.OnConnectedAsync();
    }

    public async Task BroadcastNotificationToAllClients(string message)
    {
        await Clients.All.BroadcastNotification(message);
    }

    public async Task BroadcastNotificationToSpecificUser(string userId, string message)
    {
        await Clients.User(userId).BroadcastNotification(message);
    }

    public async Task BroadcastNotificationToSpecificUsers(List<string> userIds, string message)
    {
        await Clients.Users(userIds).BroadcastNotification(message);
    }
}