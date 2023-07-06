using NutriFit.Application.Input.Commands.UserContext.Bucket;
using NutriFit.Application.Input.Receivers.Interfaces;
using NutriFit.Application.Input.Repositories;

namespace NutriFit.Application.Input.Receivers.User
{
    public class InsertOrUpdateImageReceiver : IReceiverAsync<UserImageCommand, Task<State>>
    {
        private readonly IWriteUserRepository _repository;
        public InsertOrUpdateImageReceiver(IWriteUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<State> Action(UserImageCommand command)
        {
            try
            {
                var response = await _repository.InsertOrUpdateUserImage(command);
                return response;
            }
            catch (Exception ex)
            {
                return new State(500, ex.Message, null);
            }
        }
    }
}
