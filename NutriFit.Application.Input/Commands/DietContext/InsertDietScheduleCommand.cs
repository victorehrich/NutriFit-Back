using NutriFit.Application.Input.Commands.Interfaces;

namespace NutriFit.Application.Input.Commands.DietContext
{
    public class InsertDietScheduleCommand : ICommand
    {
        public InsertDietScheduleCommand()
        {
        }

        public InsertDietScheduleCommand(int morningMealId, int afternoonMealId, int nightnMealId)
        {
            MorningMealId = morningMealId;
            AfternoonMealId = afternoonMealId;
            NightnMealId = nightnMealId;
        }

        public int MorningMealId { get; set; }
        public int AfternoonMealId { get; set; }
        public int NightnMealId { get; set; }

    }
}
