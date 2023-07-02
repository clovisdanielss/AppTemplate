using AppTemplate.Shared.Interfaces;

namespace AppTemplate.Application.Models
{
    public class Notification : INotification
    {
        public string Message { get; set; }
    }
}
