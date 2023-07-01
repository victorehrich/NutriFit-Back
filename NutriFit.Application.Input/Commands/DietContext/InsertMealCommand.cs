using NutriFit.Application.Input.Commands.DietContext.DTO;
using NutriFit.Application.Input.Commands.Interfaces;

namespace NutriFit.Application.Input.Commands.DietContext
{
    public class InsertMealCommand : ICommand
    {
        public InsertMealCommand()
        {
        }

        public InsertMealCommand(DishScheduleDietContext? dishScheduleContext)
        {
            DishScheduleContext = dishScheduleContext;
        }

        public DishScheduleDietContext? DishScheduleContext { get; set; }
    }
}
