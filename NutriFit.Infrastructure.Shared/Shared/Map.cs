using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Infrastructure.Shared.Shared
{
    public static class Map
    {
        private const string prefix = "db";
        public static string GetUserTable()
        {
            return $"{prefix}.TB_USER";
        }
        public static string GetUserSexTable()
        {
            return $"{prefix}.TB_USER_SEX";
        }
        public static string GetUserBiotypeTable()
        {
            return $"{prefix}.TB_USER_BIOTYPE";
        }
        public static string GetMealTable()
        {
            return $"{prefix}.TB_MEAL";
        }
        public static string GetDietScheduleTable()
        {
            return $"{prefix}.TB_DIET_SCHEDULE";
        }
        public static string GetDietTable()
        {
            return $"{prefix}.TB_DIET";
        }
        public static string GetUserDietTable()
        {
            return $"{prefix}.TB_USER_DIET";
        }
    }
}
