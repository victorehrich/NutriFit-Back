using NutriFit.Application.Input.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Application.Input.Commands.UserContext
{
    public class UserCommand: ICommand
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public int SexId { get; set; }

        public int BiotypeId { get; set; }

        public double Heigth { get; set; }

        public double Weigth { get; set; }

        public int Age { get; set; }

    }
}
