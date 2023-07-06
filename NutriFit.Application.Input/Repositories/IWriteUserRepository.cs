using NutriFit.Application.Input.Commands.UserContext.Bucket;
using NutriFit.Application.Input.Receivers;
using NutriFit.Domain.Entities;

namespace NutriFit.Application.Input.Repositories
{
    public interface IWriteUserRepository
    {
        void InsertUser(UserEntity user);

        void UpdateUser(UserEntity user);

        Task<State> InsertOrUpdateUserImage(UserImageCommand userImage);
    }
}
