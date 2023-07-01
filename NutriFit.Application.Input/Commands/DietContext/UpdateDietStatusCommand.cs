using NutriFit.Application.Input.Commands.Interfaces;

namespace NutriFit.Application.Input.Commands.DietContext
{
    public class UpdateDietStatusCommand : ICommand
    {
        public UpdateDietStatusCommand()
        {
        }

        public UpdateDietStatusCommand(int userId, int dietId)
        {
            UserId = userId;
            DietId = dietId;
        }

        public bool CurrentActive { get; } = true;
        public int UserId { get; set; }
        public int DietId { get; set; }


    }
}
