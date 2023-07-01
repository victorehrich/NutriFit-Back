using NutriFit.Application.Input.Commands.DietContext;
using NutriFit.Application.Input.Commands.DietContext.DTO;
using NutriFit.Application.Input.Receivers.Interfaces;
using NutriFit.Application.Input.Repositories;
using NutriFit.Domain.Entities;
using NutriFit.Domain.Notifications;

namespace NutriFit.Application.Input.Receivers.Diet
{
    public class InsertMealReceiver : IReceiver<InsertMealCommand, State>
    {
        private readonly IWriteDietRepository _repository;

        public InsertMealReceiver(IWriteDietRepository repository)
        {
            _repository = repository;
        }

        public State Action(InsertMealCommand command)
        {

            var meals = CreateMeatObject(command.DishScheduleContext!);

            if (!meals[0].IsValid() || !meals[1].IsValid() || !meals[2].IsValid())
            {
                List<Notification> _notification = meals[0].Notifications.ToList<Notification>();
                foreach(var n in meals[1].Notifications.ToList())
                {
                    _notification.Add(n);
                }
                foreach (var n in meals[2].Notifications.ToList())
                {
                    _notification.Add(n);
                }
                IReadOnlyCollection<Notification> Notifications = _notification;
                return new State(400, "Falha ao inserir. Verifique os campos", Notifications);
            }

            try
            {
                List<int> mealsResponse = _repository.InsertMeal(meals).ToList();

                var moningMealId = mealsResponse[0];
                var afternoonMealId = mealsResponse[1];
                var nightMealId = mealsResponse[2];

                var schedule = new DishScheduleEntity(moningMealId, afternoonMealId, nightMealId);
                var scheduleId = _repository.InsertDietSchedule(schedule);

                return new State(200, "Meals adicionados com sucesso", scheduleId);
            }
            catch (Exception ex)
            {
                return new State(500, ex.Message, null);
            }

        }
        private static List<MealEntity> CreateMeatObject(DishScheduleDietContext scheduleContext)
        {
            var morningDishes = new List<DishEntity>();
            var morningDish = new DishEntity();
            foreach (var commandDish in scheduleContext!.MorningMeal.Dish)
            {
                morningDish.DishQuantity = commandDish.DishQuantity;
                morningDish.DishTime = commandDish.DishTime;
                morningDish.DishName = commandDish.DishName;
                morningDishes.Add(morningDish);
            };
            var MorningMeal = new MealEntity(
                scheduleContext.MorningMeal.StartTime,
                scheduleContext.MorningMeal.EndTime,
                morningDishes
                );
            var afternoonDishes = new List<DishEntity>();
            var afternoondish = new DishEntity();
            foreach (var commandDish in scheduleContext.AfternoonMeal.Dish)
            {
                afternoondish.DishQuantity = commandDish.DishQuantity;
                afternoondish.DishTime = commandDish.DishTime;
                afternoondish.DishName = commandDish.DishName;
                afternoonDishes.Add(afternoondish);
            };
            var AfternoonMeal = new MealEntity(
                scheduleContext.AfternoonMeal.StartTime,
                scheduleContext.AfternoonMeal.EndTime,
                afternoonDishes
                );
            var nightDishes = new List<DishEntity>();
            var nightDish = new DishEntity();
            foreach (var commandDish in scheduleContext.NightMeal.Dish)
            {
                nightDish.DishQuantity = commandDish.DishQuantity;
                nightDish.DishTime = commandDish.DishTime;
                nightDish.DishName = commandDish.DishName;
                nightDishes.Add(nightDish);
            };
            var NightMeal = new MealEntity(
                scheduleContext.NightMeal.StartTime,
                scheduleContext.NightMeal.EndTime,
                nightDishes
                );
            return new List<MealEntity>() { MorningMeal, AfternoonMeal, NightMeal };
        }
    }
}
