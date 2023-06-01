using System.Data;
using Dapper;
using NutriFit.Application.Output.DTOs;
using NutriFit.Application.Output.Interfaces;
using NutriFit.Infrastructure.Output.Factory;
using NutriFit.Infrastructure.Output.Queries;

namespace NutriFit.Infrastructure.Output.Repositories
{
    public class ReadUserRepository : IReadUserRepository
    {
        private readonly IDbConnection _conn;

        public ReadUserRepository(SqlFactory factory)
        {
            _conn = factory.MySqlConnection();
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var query = new UserQueries().GetAllUsers();
            try
            {
                using(_conn)
                {
                    return _conn.Query<UserDTO>(query.Query) as List<UserDTO>;
                }
            }
            catch
            {
                throw new Exception("Falha ao recuperar os usuários");
            }
        }

        public UserDTO GetUsersByEmail(string email)
        {
            var query = new UserQueries().GetUsersByEmail(email);
            try
            {
                using (_conn)
                {
                    return _conn.QueryFirstOrDefault<UserDTO>(query.Query, query.Parameters) as UserDTO;
                }
            }
            catch
            {
                throw new Exception("Falha ao recuperar os usuário");
            }
        }

        public UserDTO GetUsersById(int id)
        {
            var query = new UserQueries().GetUsersById(id);
            try
            {
                using (_conn)
                {
                    return _conn.QueryFirstOrDefault<UserDTO>(query.Query, query.Parameters) as UserDTO;
                }
            }
            catch
            {
                throw new Exception("Falha ao recuperar os usuário");
            }
        }
    }
}
