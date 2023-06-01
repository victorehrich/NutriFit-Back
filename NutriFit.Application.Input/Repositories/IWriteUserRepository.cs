using NutriFit.Domain.Entities;

namespace NutriFit.Application.Input.Repositories
{
    public interface IWriteUserRepository
    {
        void InsertUser(UserEntity user);
    }
}
