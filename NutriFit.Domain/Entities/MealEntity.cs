using NutriFit.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Domain.Entities
{
    public class MealEntity : BaseEntity
    {
        public MealEntity(int id, TimeOnly startTime, TimeOnly endTime, List<DishEntity> dish)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            Dish = dish;
            _notifications = new List<Notification>();
            IsValid();

        }
        public IReadOnlyCollection<Notification> Notifications => _notifications;

        List<Notification> _notifications;
        public int Id { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public List<DishEntity> Dish { get; set; }
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
