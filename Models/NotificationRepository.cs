using System.Collections.Generic;
using System.Linq;

namespace BloggingApp.Models
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext context;

        public NotificationRepository(AppDbContext _context)
        {
            context = _context;
        }
        public Notification AddNotification(Notification notification)
        {
            context.Notifications.Add(notification);
            context.SaveChanges();
            return notification;
        }

        public IEnumerable<Notification> AllNotifications(int userId)
        {
            return context.Notifications.Where(n => n.userId == userId).Select(n => n); ;
        }
    }
}