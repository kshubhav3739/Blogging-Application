using System.Collections.Generic;

namespace BloggingApp.Models
{
    public interface INotificationRepository
    {
        Notification AddNotification(Notification notification);
        IEnumerable<Notification> AllNotifications(int userId);
    }
}