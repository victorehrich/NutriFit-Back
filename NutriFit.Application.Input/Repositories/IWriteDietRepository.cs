using NutriFit.Domain.Entities;

namespace NutriFit.Application.Input.Repositories
{
    public interface IWriteDietRepository
    {
        void UpdateOldDietsStatus(int userId);
        int InsertDiet(DietEntity diet);
        List<int> InsertMeal(IEnumerable<MealEntity> meals);
        int InsertDietSchedule (DishScheduleEntity schedule);
        int InsertUserDiet (UserDietEntity userDiet);
        void UpdateDietStatus(int dietId);
    }
}
