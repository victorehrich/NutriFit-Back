using NutriFit.Application.Output.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Application.Output.Interfaces
{
    public interface IReadUserRepository
    {
        IEnumerable<UserDTO> GetUsers();
        UserDTO GetUsersById(int id);
        UserDTO GetUsersByEmail(string email);

    }
}
