using DAL.EF;
using DAL.EF.Tables;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    public class NotificationRepo
    {
        OnlineFoodOrderingSystemDbContext db;

        public NotificationRepo(OnlineFoodOrderingSystemDbContext db)
        {
            this.db = db;
        }

        public bool Create(Notification n)
        {
            db.Notifications.Add(n);
            return db.SaveChanges() > 0;
        }

        public List<Notification> GetByUser(int userId)
        {
            return db.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();
        }

        public int GetUnreadCount(int userId)
        {
            return db.Notifications
                .Count(n => n.UserId == userId && n.IsRead == false);
        }

        public bool MarkAsRead(int id)
        {
            var n = db.Notifications.Find(id);
            if (n == null) return false;
            n.IsRead = true;
            return db.SaveChanges() > 0;
        }

        public bool MarkAllAsRead(int userId)
        {
            var unread = db.Notifications
                .Where(n => n.UserId == userId && n.IsRead == false)
                .ToList();
            unread.ForEach(n => n.IsRead = true);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var n = db.Notifications.Find(id);
            if (n == null) return false;
            db.Notifications.Remove(n);
            return db.SaveChanges() > 0;
        }
    }
}