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
                using (_conn)
                {
                    QueryModel query = new DietQueries().GetAllMorningMeals(dietScheduleId);
                    var queryReturnHandler = _conn.QuerySingleOrDefault(query.Query);
                    dishSchedule.MorningMeal = new MealDTO
                    {
                        StartTime = queryReturnHandler.StartTime,
                        EndTime = queryReturnHandler.EndTime,
                        MealId = queryReturnHandler.MealId,
                        Dish = JsonSerializer.Deserialize<List<DishDTO>>(queryReturnHandler.Dish)
                    };
                    query = new DietQueries().GetAllAfternoonMeals(dietScheduleId);
                    queryReturnHandler = _conn.QuerySingleOrDefault(query.Query);
                    dishSchedule.AfternoonMeal = new MealDTO
                    {
                        StartTime = queryReturnHandler.StartTime,
                        EndTime = queryReturnHandler.EndTime,
                        MealId = queryReturnHandler.MealId,
                        Dish = new List<DishDTO>(JsonSerializer.Deserialize<DishDTO[]>(queryReturnHandler.Dish))
                    };
                    query = new DietQueries().GetAllNightMeals(dietScheduleId);
                    queryReturnHandler = _conn.QuerySingleOrDefault(query.Query);
                    dishSchedule.NightMeal = new MealDTO
                    {
                        StartTime = queryReturnHandler.StartTime,
                        EndTime = queryReturnHandler.EndTime,
                        MealId = queryReturnHandler.MealId,
                        Dish = new List<DishDTO>(JsonSerializer.Deserialize<DishDTO[]>(queryReturnHandler.Dish))
                    };
                    return dishSchedule;
                }
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
                using (_conn)
                {
                    return _conn.Query<DietsDTO>(query.Query) as List<DietsDTO>;
                }
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
    }
}
