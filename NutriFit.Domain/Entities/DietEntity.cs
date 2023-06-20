using NutriFit.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Domain.Entities
{
    public class DietEntity : BaseEntity
    {
        public IReadOnlyCollection<Notification> Notifications => _notifications;

        List<Notification> _notifications;

        public DietEntity(int id, bool currentActive, int mondayScheduleId, int tuesdayScheduleId, int wednesdayScheduleId, int thursdayScheduleId, int fridayScheduleId, int saturdayScheduleId, int sundayScheduleId)
        {
            _notifications = new List<Notification>();
            Id = id;
            CurrentActive = currentActive;
            MondayScheduleId = mondayScheduleId;
            TuesdayScheduleId = tuesdayScheduleId;
            WednesdayScheduleId = wednesdayScheduleId;
            ThursdayScheduleId = thursdayScheduleId;
            FridayScheduleId = fridayScheduleId;
            SaturdayScheduleId = saturdayScheduleId;
            SundayScheduleId = sundayScheduleId;
        }

        public int Id { get; set; }
        public bool CurrentActive { get; set; }
        public int MondayScheduleId { get; set; }
        public int TuesdayScheduleId { get; set; }
        public int WednesdayScheduleId { get; set; }
        public int ThursdayScheduleId { get; set; }
        public int FridayScheduleId { get; set; }
        public int SaturdayScheduleId { get; set; }
        public int SundayScheduleId { get; set; }

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
