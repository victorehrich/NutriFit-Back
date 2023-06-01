using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Infrastructure.Output.Factory
{
    public class SqlFactory
    {
        public IDbConnection MySqlConnection()
        {
            var conn = new MySqlConnection("server=127.0.0.1;port=3306; uid=user; pwd=password; database=db");
            return conn;
        }
    }
}