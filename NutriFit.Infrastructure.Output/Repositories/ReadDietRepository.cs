using System.Data;
using System.Text.Json;
using Dapper;
using NutriFit.Application.Output.DTOs;
using NutriFit.Application.Output.Interfaces;
using NutriFit.Infrastructure.Output.Factory;
using NutriFit.Infrastructure.Output.Queries;
using NutriFit.Infrastructure.Shared.Shared;

namespace NutriFit.Infrastructure.Output.Repositories
{
    public class ReadDietRepository : IReadDietRepository
    {
        private readonly IDbConnection _conn;

        public ReadDietRepository(SqlFactory factory)
        {
            _conn = factory.MySqlConnection();
        }

        public DishScheduleDTO GetDietSchedule(int dietScheduleId)
        {
            var dishSchedule = new DishScheduleDTO();
            try
            {
                _conn.Open();
                QueryModel query = new DietQueries().GetAllMorningMeals(dietScheduleId);
                var queryReturnHandler = _conn.QuerySingleOrDefault(query.Query);
                _conn.Close();
                dishSchedule.MorningMeal = new MealDTO
                {
                    StartTime = queryReturnHandler.StartTime,
                    EndTime = queryReturnHandler.EndTime,
                    MealId = queryReturnHandler.MealId,
                    Dish = JsonSerializer.Deserialize<List<DishDTO>>(queryReturnHandler.Dish)
                };
                _conn.Open();
                query = new DietQueries().GetAllAfternoonMeals(dietScheduleId);
                queryReturnHandler = _conn.QuerySingleOrDefault(query.Query);
                _conn.Close();
                dishSchedule.AfternoonMeal = new MealDTO
                {
                    StartTime = queryReturnHandler.StartTime,
                    EndTime = queryReturnHandler.EndTime,
                    MealId = queryReturnHandler.MealId,
                    Dish = new List<DishDTO>(JsonSerializer.Deserialize<DishDTO[]>(queryReturnHandler.Dish))
                };
                query = new DietQueries().GetAllNightMeals(dietScheduleId);
                _conn.Open();
                queryReturnHandler = _conn.QuerySingleOrDefault(query.Query);
                _conn.Close();
                dishSchedule.NightMeal = new MealDTO
                {
                    StartTime = queryReturnHandler.StartTime,
                    EndTime = queryReturnHandler.EndTime,
                    MealId = queryReturnHandler.MealId,
                    Dish = new List<DishDTO>(JsonSerializer.Deserialize<DishDTO[]>(queryReturnHandler.Dish))
                };
                return dishSchedule;

            }
            catch
            {
                throw new Exception("Falha ao recuperar as informações das dietas");
            }
        }

        public IEnumerable<DietsDTO> GetDiets(int userId)
        {
            var query = new DietQueries().GetAllDiets(userId);
            try
            {
                _conn.Open();
                var response = _conn.Query<DietsDTO>(query.Query) as List<DietsDTO>;
                _conn.Close();

                return response;
            }
            catch
            {
                throw new Exception("Falha ao recuperar as dietas");
            }
        }

        public DietsDTO GetDiet(int dietId)
        {
            var query = new DietQueries().GetDiet(dietId);
            try
            {
                using (_conn)
                {
                    return _conn.QueryFirst<DietsDTO>(query.Query) as DietsDTO;
                }
            }
            catch
            {
                throw new Exception("Falha ao recuperar a dieta");
            }
        }

        public DishScheduleDTO GetTodayDiet(DietsDTO currentDiet)
        {
            DateTime dateValue = DateTime.Now;
            int scheduleId = 0;
            switch ((int)dateValue.DayOfWeek)
            {
                case 0:
                    scheduleId = currentDiet.SundayScheduleId;
                    break;
                case 1:
                    scheduleId = currentDiet.MondayScheduleId;
                    break;
                case 2:
                    scheduleId = currentDiet.TuesdayScheduleId;
                    break;
                case 3:
                    scheduleId = currentDiet.WednesdayScheduleId;
                    break;
                case 4:
                    scheduleId = currentDiet.TuesdayScheduleId;
                    break;
                case 5:
                    scheduleId = currentDiet.FridayScheduleId;
                    break;
                case 6:
                    scheduleId = currentDiet.SaturdayScheduleId;
                    break;
            }
            return this.GetDietSchedule(scheduleId);
            throw new NotImplementedException();
        }
    }
}
