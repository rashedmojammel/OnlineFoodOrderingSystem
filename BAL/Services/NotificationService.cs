using AutoMapper;
using BAL.DTOs;
using DAL.EF.Tables;
using DAL.Repos;
using System.Collections.Generic;

namespace BAL.Services
{
    public class NotificationService
    {
        NotificationRepo repo;
        Mapper mapper;

        public NotificationService(NotificationRepo repo)
        {
            this.repo = repo;
            mapper = MapperConfig.GetMapper();
        }

        public bool Send(int userId, string title, string message)
        {
            var n = new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                IsRead = false,
                CreatedAt = DateTime.Now
            };
            return repo.Create(n);
        }

        public List<NotificationDTO> GetByUser(int userId)
        {
            var data = repo.GetByUser(userId);
            return mapper.Map<List<NotificationDTO>>(data);
        }

        public int GetUnreadCount(int userId)
        {
            return repo.GetUnreadCount(userId);
        }

        public bool MarkAsRead(int id)
        {
            return repo.MarkAsRead(id);
        }

        public bool MarkAllAsRead(int userId)
        {
            return repo.MarkAllAsRead(userId);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}