using NutriFit.Application.Input.Repositories;
using NutriFit.Domain.Entities;
using NutriFit.Infrastructure.Input.Factory;
using NutriFit.Infrastructure.Input.Queries;
using System.Data;
using Dapper;
using MySqlConnector;

namespace NutriFit.Infrastructure.Input.Repositories
{
    public class WriteDietRepository : IWriteDietRepository
    {
        private readonly IDbConnection _conn;

        public WriteDietRepository(SqlFactory factory)
        {
            _conn = factory.MySqlConnection();
        }

        public int InsertDiet(DietEntity diet)
        {
            var query = new DietQuery().InsertDietQuery(diet);
            try
            {
                _conn.Open();
                int dietId = _conn.QuerySingle<int>(query.Query, query.Parameters);
                _conn.Close();
                return dietId;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir o dieta");
            }
        }
        public void UpdateOldDietsStatus(int userId)
        {
            var query = new DietQuery().UpdateOldDietsStatusQuery(userId);
            try
            {
                _conn.Open();
                _conn.Execute(query.Query, query.Parameters);
                _conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o status das dietas");
            }
        }

        public int InsertDietSchedule(DishScheduleEntity schedule)
        {
            var query = new DietQuery().InsertDietScheduleQuery(schedule);
            try
            {
                _conn.Open();
                var id = _conn.QuerySingle<int>(query.Query, query.Parameters);
                _conn.Close();
                return id;

            }
            catch (Exception ex)
            {
                _conn.Close();
                throw new Exception("Erro ao inserir o Meal");
            }
        }

        public List<int> InsertMeal(IEnumerable<MealEntity> meals)
        {
            List<int> ids = new List<int>();
            _conn.Open();
            foreach (var meal in meals)
            {
                var query = new DietQuery().InsertMealQuery(meal);
                try
                {
                    var id = _conn.QuerySingle<int>(query.Query, query.Parameters);
                    ids.Add(id);
                }
                catch (Exception ex)
                {
                    _conn.Close();
                    throw new Exception("Erro ao inserir o Meal");
                }

            }
            _conn.Close();

            if (ids.Count == 0) throw new Exception("Internal server error");
            return ids;

        }

        public int InsertUserDiet(UserDietEntity userDiet)
        {
            var query = new DietQuery().InsertUserDietQuery(userDiet);
            try
            {
                _conn.Open();
                var id = _conn.QuerySingle<int>(query.Query, query.Parameters);
                _conn.Close();
                return id;
                
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao linkar a dieta com o usuário");
            }
        }

        public void UpdateDietStatus(int dietId)
        {
            var query = new DietQuery().UpdateDietStatusQuery(dietId);
            try
            {
                _conn.Open();
                _conn.QuerySingle<int>(query.Query, query.Parameters);
                _conn.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o status da dieta");
            }
        }
    }
}
