using NutriFit.Application.Input.Commands.DietContext;
using NutriFit.Application.Input.Commands.DietContext.DTO;
using NutriFit.Application.Input.Receivers.Interfaces;
using NutriFit.Application.Input.Repositories;
using NutriFit.Domain.Entities;
using NutriFit.Domain.Notifications;

namespace NutriFit.Application.Input.Receivers.Diet
{
    public class UpdateDietStatusReceiver : IReceiver<UpdateDietStatusCommand, State>
    {
        private readonly IWriteDietRepository _repository;

        public UpdateDietStatusReceiver(IWriteDietRepository repository)
        {
            _repository = repository;
        }

        public State Action(UpdateDietStatusCommand command)
        {

            try
            {
                _repository.UpdateOldDietsStatus(command.UserId);

                _repository.UpdateDietStatus(command.DietId);

                return new State(200, "Status alterado com sucesso", null);
            }
            catch (Exception ex)
            {
                return new State(500, ex.Message, null);
            }

        }     
    }           
}               
                
                
                
                
                
                