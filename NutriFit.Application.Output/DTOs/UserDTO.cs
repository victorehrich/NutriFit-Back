using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Application.Output.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public int SexId { get; set; }

        public string SexName { get; set; }

        public int BiotypeId { get; set; }

        public string BiotypeName { get; set; }

        public double Heigth { get; set; }

        public double Weigth { get; set; }

        public int Age { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
