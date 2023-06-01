﻿using NutriFit.Application.Input.Commands.UserContext;
using NutriFit.Application.Input.Receivers.Interfaces;
using NutriFit.Application.Input.Repositories;
using NutriFit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Application.Input.Receivers
{
    public class InsertUserReceiver : IReceiver<UserCommand, State>
    {
        private readonly IWriteUserRepository _repository;

        public InsertUserReceiver(IWriteUserRepository repository)
        {
            _repository = repository;
        }

        public State Action(UserCommand command)
        {
            var user = new UserEntity(command.Email,
                                      command.Name,
                                      command.Password,
                                      command.SexId,
                                      command.BiotypeId,
                                      command.Heigth,
                                      command.Weigth,
                                      command.Age);
            if (!user.IsValid())
            {
                return new State(400, "Falha ao inserir. Verifique os campos", user.Notifications);
            }
            try
            {
                _repository.InsertUser(user);
                return new State(200, "Usuário adicionado com sucesso", user);
            }
            catch(Exception ex)
            {
                return new State(500, ex.Message, null);
            }

        }
    }
}
