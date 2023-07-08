using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Infrastructure.Input.Factory
{
    public class SqlFactory
    {
        public IDbConnection MySqlConnection()
        {
            // var conn = new MySqlConnection("server=localhost;port=3306; uid=user; pwd=password; database=db");
            //var conn = new MySqlConnection("server=mysql;port=3306; uid=user; pwd=password; database=db");
            var conn = new MySqlConnection("server=database-1.cw6cnez26jd0.us-east-1.rds.amazonaws.com; port=3306; uid=admin; pwd=asdf1234; database=db");
            return conn;
        }
    }
}
