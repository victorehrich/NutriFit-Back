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
    }
}
