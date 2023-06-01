using NutriFit.Infrastructure.Shared.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Infrastructure.Output.Queries.Interfaces
{
    public interface IUserQueries
    {
        QueryModel GetAllUsers();
        QueryModel GetUsersById(int id);
        QueryModel GetUsersByEmail(string email);

    }
}
