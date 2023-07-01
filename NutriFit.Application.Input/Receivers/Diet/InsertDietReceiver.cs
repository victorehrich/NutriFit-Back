using NutriFit.Application.Input.Commands.DietContext;
using NutriFit.Application.Input.Commands.DietContext.DTO;
using NutriFit.Application.Input.Receivers.Interfaces;
using NutriFit.Application.Input.Repositories;
using NutriFit.Domain.Entities;
using NutriFit.Domain.Notifications;

namespace NutriFit.Application.Input.Receivers.Diet
{
    public class InsertDietReceiver : IReceiver<InsertDietCommand, State>
    {
        private readonly IWriteDietRepository _repository;

        public InsertDietReceiver(IWriteDietRepository repository)
        {
            _repository = repository;
        }

        public State Action(InsertDietCommand command)
        {

            var diet = CreateDietObject(command);

            if (!diet.IsValid())
            {
                return new State(400, "Falha ao inserir. Verifique os campos", diet.Notifications);
            }

            try
            {
                _repository.UpdateOldDietsStatus(command.UserId);

                int dietId = _repository.InsertDiet(diet);

                var userDiet = new UserDietEntity(command.UserId, dietId);
                var userDietId = _repository.InsertUserDiet(userDiet);
                userDiet.Id = userDietId;
                return new State(200, "Dieta adicionado com sucesso", userDiet);
            }
            catch (Exception ex)
            {
                return new State(500, ex.Message, null);
            }

        }
        private static DietEntity CreateDietObject(InsertDietCommand command)
        {
            return new DietEntity(
                command.CurrentActive,
                command.DietName,
                command.DietGoal,
                command.MondayScheduleId,
                command.TuesdayScheduleId,
                command.WednesdayScheduleId,
                command.ThursdayScheduleId,
                command.FridayScheduleId,
                command.SaturdayScheduleId,
                command.SundayScheduleId
                );
        }       
    }           
}               
                
                
                
                
                
                