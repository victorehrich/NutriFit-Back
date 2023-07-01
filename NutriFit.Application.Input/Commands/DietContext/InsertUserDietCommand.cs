using NutriFit.Application.Input.Commands.Interfaces;

namespace NutriFit.Application.Input.Commands.DietContext
{
    public class InsertUserDietCommand : ICommand
    {
        public InsertUserDietCommand()
        {
        }

        public InsertUserDietCommand(int userId, int dietId)
        {
            UserId = userId;
            DietId = dietId;
        }

        public int UserId { get; set; }
        public int DietId { get; set; }

    }
}
