using NutriFit.Application.Input.Commands.Interfaces;

namespace NutriFit.Application.Input.Commands.DietContext
{
    public class InsertDietCommand : ICommand
    {
        public InsertDietCommand()
        {
        }

        public InsertDietCommand(int userId, string dietName, string dietGoal, int mondayScheduleId, int tuesdayScheduleId, int wednesdayScheduleId, int thursdayScheduleId, int fridayScheduleId, int saturdayScheduleId, int sundayScheduleId)
        {
            UserId = userId;
            DietName = dietName;
            DietGoal = dietGoal;
            MondayScheduleId = mondayScheduleId;
            TuesdayScheduleId = tuesdayScheduleId;
            WednesdayScheduleId = wednesdayScheduleId;
            ThursdayScheduleId = thursdayScheduleId;
            FridayScheduleId = fridayScheduleId;
            SaturdayScheduleId = saturdayScheduleId;
            SundayScheduleId = sundayScheduleId;
        }
        public int UserId { get; set; }
        public string DietName { get; set; }
        public string DietGoal { get; set; }
        public bool CurrentActive { get; } = true;
        public int MondayScheduleId { get; set; }
        public int TuesdayScheduleId { get; set; }
        public int WednesdayScheduleId { get; set; }
        public int ThursdayScheduleId { get; set; }
        public int FridayScheduleId { get; set; }
        public int SaturdayScheduleId { get; set; }
        public int SundayScheduleId { get; set; }

    }
}
