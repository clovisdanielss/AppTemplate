namespace AppTemplate.Shared.Interfaces;

public interface INotifier
{
    void Notify(string notification);
    ICollection<INotification> GetNotifications();
}

public interface INotification
{
    string Message { get; set; }
}