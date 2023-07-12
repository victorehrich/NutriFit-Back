using NutriFit.Application.Output.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Application.Output.Interfaces
{
    public interface IReadDietRepository
    {
        IEnumerable<DietsDTO> GetDiets(int userId);
        DietsDTO GetDiet(int dietId);
        DishScheduleDTO GetDietSchedule(int dietScheduleId);
        DishScheduleDTO GetTodayDiet(DietsDTO currentDiet);


    }
}
