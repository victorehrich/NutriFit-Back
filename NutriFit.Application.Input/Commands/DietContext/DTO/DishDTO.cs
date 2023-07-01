using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NutriFit.Application.Input.Commands.DietContext.DTO
{
    public class DishDietContext
    {
        [JsonPropertyName("DishName")]
        public string DishName { get; set; }
        [JsonPropertyName("DishQuantity")]
        public string DishQuantity { get; set; }
        [JsonPropertyName("DishTime")]
        public string DishTime { get; set; }
    }
}
