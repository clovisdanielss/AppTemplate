using AppTemplate.Shared.Models;
using AppTemplate.Shared.Interfaces;

namespace AppTemplate.Shared.Services;

public class NotifierService : INotifier
{
    private ICollection<INotification> _notifications;

    public NotifierService()
    {
        _notifications = new List<INotification>();
    }

    public ICollection<INotification> GetNotifications()
    {
        return _notifications;
    }

    public void Notify(string notification)
    {
        _notifications.Add(
            new Notification
            {
                Message = notification,
            }
        );
    }
}