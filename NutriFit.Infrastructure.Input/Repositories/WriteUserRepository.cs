using NutriFit.Application.Input.Repositories;
using NutriFit.Domain.Entities;
using NutriFit.Infrastructure.Input.Factory;
using NutriFit.Infrastructure.Input.Queries;
using System.Data;
using Dapper;
using MySqlConnector;

namespace NutriFit.Infrastructure.Input.Repositories
{
    public class WriteUserRepository : IWriteUserRepository
    {
        private readonly IDbConnection _conn;

        public WriteUserRepository(SqlFactory factory)
        {
            _conn = factory.MySqlConnection();
        }

        public void InsertUser(UserEntity user)
        {
            var query = new UserQuery().InsertUserQuery(user);
            try
            {
                using (_conn)
                {
                    _conn.Execute(query.Query, query.Parameters);
                }
            }
            catch(Exception ex)
            {
                if(ex.Message.Contains("Duplicate entry"))
                {
                    throw new Exception("Email já existente");
                }
                throw new Exception("Erro ao inserir o usuário");
            }
            
        }

        public void UpdateUser(UserEntity user)
        {
            var query = new UserQuery().UpdateUserQuery(user);
            try
            {
                using (_conn)
                {
                    _conn.Execute(query.Query, query.Parameters);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry"))
                {
                    throw new Exception("Email já existente");
                }
                throw new Exception("Erro ao inserir o usuário");
            }
        }
    }
}
