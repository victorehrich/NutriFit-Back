using NutriFit.Infrastructure.Shared.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Infrastructure.Output.Queries.Interfaces
{
    public interface IDietQueries
    {
        QueryModel GetAllDiets(int userId);
        QueryModel GetDiet(int dietId);
        QueryModel GetAllMorningMeals(int dietScheduleId);
        QueryModel GetAllAfternoonMeals(int dietScheduleId);
        QueryModel GetAllNightMeals(int dietScheduleId);

    }
}
