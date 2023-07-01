using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Application.Input.Commands.DietContext.DTO
{
    public class MealDietContext
    {
        public int MealId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public List<DishDietContext> Dish { get; set; }
    }
}
