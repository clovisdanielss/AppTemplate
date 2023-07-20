using AppTemplate.Shared.Interfaces;

namespace AppTemplate.Shared.Models
{
    public class Notification : INotification
    {
        public string Message { get; set; }
    }
}
