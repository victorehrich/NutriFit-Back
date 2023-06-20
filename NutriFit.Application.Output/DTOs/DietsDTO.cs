using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Application.Output.DTOs
{
    public class DietsDTO
    {
        public int DietId { get; set; }
        public int UserId { get; set; }
        public string DietName { get; set; }
        public string DietGoal { get; set; }
        public bool CurrentActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int MondayScheduleId { get; set; }
        public int TuesdayScheduleId { get; set; }
        public int WednesdayScheduleId { get; set; }
        public int ThursdayScheduleId { get; set; }
        public int FridayScheduleId { get; set; }
        public int SaturdayScheduleId { get; set; }
        public int SundayScheduleId { get; set; }
    }
}
