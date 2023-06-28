using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;


// ReSharper disable once CheckNamespace
namespace SignalRChat.Hubs;

public class NotificationHub : Hub
{
    private readonly ILogger<NotificationHub> _logger;
    private string _currentValue;
    public NotificationHub(ILogger<NotificationHub> logger)
    {
        _logger = logger;
        _currentValue = "ctor";
        _logger.LogInformation("NotificationHub ctor");
    }

    public async Task UpdateValue(string value)
    {
        _currentValue = value;
        _logger.LogInformation("updated current value to {currentValue}", _currentValue);
        await Clients.All.SendAsync("CurrentValue", _currentValue);
    }
    
    public async Task GetValue()
    {
        _logger.LogInformation("Get Value received");
        await Clients.All.SendAsync("CurrentValue", _currentValue);
    }
}