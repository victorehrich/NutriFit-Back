using NutriFit.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Domain.Entities
{
    public class DishScheduleEntity : BaseEntity
    {
        public DishScheduleEntity(int id, int morningMealId, int afternoonMealId, int nightnMealId)
        {
            Id = id;
            MorningMealId = morningMealId;
            AfternoonMealId = afternoonMealId;
            NightnMealId = nightnMealId;
            _notifications = new List<Notification>();
            IsValid();

        }
        public DishScheduleEntity(int morningMealId, int afternoonMealId, int nightnMealId)
        {
            MorningMealId = morningMealId;
            AfternoonMealId = afternoonMealId;
            NightnMealId = nightnMealId;
            _notifications = new List<Notification>();
            IsValid();

        }
        public IReadOnlyCollection<Notification> Notifications => _notifications;

        List<Notification> _notifications;
        public int Id { get; set; }
        public int MorningMealId { get; set; }
        public int AfternoonMealId { get; set; }
        public int NightnMealId { get; set; }

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
