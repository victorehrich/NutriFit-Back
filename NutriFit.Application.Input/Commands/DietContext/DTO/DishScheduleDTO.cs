namespace NutriFit.Application.Input.Commands.DietContext.DTO
{
    public class DishScheduleDietContext
    {
        public MealDietContext MorningMeal { get; set; }
        public MealDietContext AfternoonMeal { get; set; }
        public MealDietContext NightMeal { get; set; }
    }
}
