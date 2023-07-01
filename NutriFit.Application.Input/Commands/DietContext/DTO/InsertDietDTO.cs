using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NutriFit.Application.Input.Commands.DietContext.DTO
{
    public class InsertDietDTO
    {
        public string DietName { get; set; }
        public string DietGoal { get; set; }
        public List<string> DietAllergies { get; set; }
    }
}
