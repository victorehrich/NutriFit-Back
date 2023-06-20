using NutriFit.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Domain.Entities
{
    public class UserDietEntity : BaseEntity
    {
        public IReadOnlyCollection<Notification> Notifications => _notifications;

        List<Notification> _notifications;

        public UserDietEntity(int id, int userId, int dietId)
        {
            Id = id;
            UserId = userId;
            DietId = dietId;
            _notifications = new List<Notification>();
            IsValid();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int DietId { get; set; }

        public void PullNotification(Notification notification)
        {
            this._notifications.Add(notification);
        }
        public bool IsValid()
        {
            return true;
        }
    }
}
