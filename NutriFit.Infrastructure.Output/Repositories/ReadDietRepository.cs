using System.Data;
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

        public DishScheduleDTO GetDiet(int dietScheduleId)
        {
            var dishSchedule = new DishScheduleDTO();
            try
            {
                using (_conn)
                {
                    QueryModel query = new DietQueries().GetAllMorningMeals(dietScheduleId);
                    var queryReturnHandler = _conn.QuerySingleOrDefault<MealDTO>(query.Query);
                    dishSchedule.MorningMeal = _conn.QuerySingleOrDefault<MealDTO>(query.Query) as MealDTO;
                    query = new DietQueries().GetAllAfternoonMeals(dietScheduleId);
                    dishSchedule.AfternoonMeal = _conn.QuerySingleOrDefault<MealDTO>(query.Query) as MealDTO;
                    query = new DietQueries().GetAllNightMeals(dietScheduleId);
                    dishSchedule.NightnMeal = _conn.QuerySingleOrDefault<MealDTO>(query.Query) as MealDTO;

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
    }
}
