using NutriFit.Infrastructure.Output.Queries.Interfaces;
using NutriFit.Infrastructure.Shared.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Infrastructure.Output.Queries
{
    public class UserQueries : BaseQuery, IUserQueries
    {
        public QueryModel GetAllUsers()
        {
            this.Table = Map.GetUserTable();
            this.InnerTable.Add(Map.GetUserSexTable());
            this.InnerTable.Add(Map.GetUserBiotypeTable());
            this.Query = $@"
            SELECT 
                U.ID AS Id,
                U.EMAIL AS Email,
                U.NAME AS Name,
                U.PASSWORD AS Password,
                U.SEXID AS SexId,
                US.SEXNAME AS SexName,
                U.BIOTYPEID AS BiotypeId,
                UB.BIOTYPENAME as BiotypeName,
                U.HEIGTH AS Heigth,
                U.WEIGTH AS Weigth,
                U.AGE AS Age,
                U.CREATEDON AS CreatedOn
            FROM {this.Table} AS U
            INNER JOIN {this.InnerTable[0]} AS US
            ON U.SEXID = US.ID
            INNER JOIN {this.InnerTable[1]} AS UB
            ON U.BIOTYPEID = UB.ID
            ";
            return new QueryModel(this.Query, null);
        }

        public QueryModel GetUsersByEmail(string email)
        {
            this.Table = Map.GetUserTable();
            this.InnerTable = new List<string>();
            this.InnerTable.Add(Map.GetUserSexTable());
            this.InnerTable.Add(Map.GetUserBiotypeTable());
            this.Query = $@"
            SELECT 
                U.ID AS Id,
                U.EMAIL AS Email,
                U.NAME AS Name,
                U.PASSWORD AS Password,
                U.SEXID AS SexId,
                US.SEXNAME AS SexName,
                U.BIOTYPEID AS BiotypeId,
                UB.BIOTYPENAME as BiotypeName,
                U.HEIGTH AS Heigth,
                U.WEIGTH AS Weigth,
                U.AGE AS Age,
                U.CREATEDON AS CreatedOn
            FROM {this.Table} AS U
            INNER JOIN {this.InnerTable[0]} AS US
            ON U.SEXID = US.ID
            INNER JOIN {this.InnerTable[1]} AS UB
            ON U.BIOTYPEID = UB.ID
            WHERE
            U.EMAIL = @UserEmail
            ";
            this.Parameters = new
            {
                UserEmail = email
            };
            return new QueryModel(this.Query, this.Parameters);
        }

        public QueryModel GetUsersById(int id)
        {
            this.Table = Map.GetUserTable();
            this.InnerTable = new List<string>();
            this.InnerTable.Add(Map.GetUserSexTable());
            this.InnerTable.Add(Map.GetUserBiotypeTable());
            this.Query = $@"
            SELECT 
                U.ID AS Id,
                U.EMAIL AS Email,
                U.NAME AS Name,
                U.PASSWORD AS Password,
                U.SEXID AS SexId,
                US.SEXNAME AS SexName,
                U.BIOTYPEID AS BiotypeId,
                UB.BIOTYPENAME as BiotypeName,
                U.HEIGTH AS Heigth,
                U.WEIGTH AS Weigth,
                U.AGE AS Age,
                U.CREATEDON as CreatedOn
            FROM {this.Table} AS U
            INNER JOIN {this.InnerTable[0]} AS US
            ON U.SEXID = US.ID
            INNER JOIN {this.InnerTable[1]} AS UB
            ON U.BIOTYPEID = UB.ID
            WHERE
            U.ID = @UserId
            ";
            this.Parameters = new
            {
                UserId = id
            };
            return new QueryModel(this.Query, this.Parameters);
        }
    }
}
