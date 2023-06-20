using NutriFit.Infrastructure.Output.Queries.Interfaces;
using NutriFit.Infrastructure.Shared.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Infrastructure.Output.Queries
{
    public class DietQueries : BaseQuery, IDietQueries
    {
        public QueryModel GetAllDiets(int userId)
        {
            this.InnerTable = new List<string>();
            this.Table = Map.GetUserDietTable();
            this.InnerTable.Add(Map.GetDietTable());
            this.Query = $@"
            SELECT 
                UD.DIETID AS DietId, 
                UD.USERID AS UserId,
                D.DIETNAME as DietName,
                D.DIETGOAL AS DietGoal,
                D.CURRENTACTIVE AS CurrentActive,
                D.CREATEDON AS CreatedOn,
                D.MONDAYSCHEDULEID AS MondayScheduleId,
                D.TUESDAYSCHEDULEID AS TuesdayScheduleId,
                D.WEDNESDAYSCHEDULEID AS WednesdayScheduleId,
                D.THURSDAYSCHEDULEID AS ThursdayScheduleId,
                D.FRIDAYSCHEDULEID AS FridayScheduleId,
                D.SATURDAYSCHEDULEID AS SaturdayScheduleId,
                D.SUNDAYSCHEDULEID as SundayScheduleId
            FROM {this.Table} UD
            JOIN {this.InnerTable[0]} D 
            ON UD.DIETID = D.ID
            WHERE
            UD.USERID = {userId}
            ORDER BY D.CREATEDON DESC
            ";
            return new QueryModel(this.Query, null);
        }
        public QueryModel GetAllMorningMeals(int dishScheduleId)
        {
            this.InnerTable = new List<string>();
            this.Table = Map.GetDietScheduleTable();
            this.InnerTable.Add(Map.GetMealTable());
            this.Query = $@"
            SELECT 
                M.ID as MealId,
                M.DISH as Dish,
                M.STARTTIME as StartTime,
                M.ENDTIME as EndTime
            FROM
            db.TB_DIET_SCHEDULE DS
            JOIN db.TB_MEAL M ON M.ID = DS.MORNINGMEALID
            WHERE DS.ID = {dishScheduleId}
            ";
            return new QueryModel(this.Query, null);
        }
        public QueryModel GetAllAfternoonMeals(int dishScheduleId)
        {
            this.InnerTable = new List<string>();
            this.Table = Map.GetDietScheduleTable();
            this.InnerTable.Add(Map.GetMealTable());
            this.Query = $@"
            SELECT 
                M.ID as MealId,
                M.DISH as Dish,
                M.STARTTIME as StartTime,
                M.ENDTIME as EndTime
            FROM
            db.TB_DIET_SCHEDULE DS
            JOIN db.TB_MEAL M ON M.ID = DS.AFTERNOONMEALID
            WHERE DS.ID = {dishScheduleId}
            ";
            return new QueryModel(this.Query, null);
        }
        public QueryModel GetAllNightMeals(int dishScheduleId)
        {
            this.InnerTable = new List<string>();
            this.Table = Map.GetDietScheduleTable();
            this.InnerTable.Add(Map.GetMealTable());
            this.Query = $@"
            SELECT 
                M.ID as MealId,
                M.DISH as Dish,
                M.STARTTIME as StartTime,
                M.ENDTIME as EndTime
            FROM
            db.TB_DIET_SCHEDULE DS
            JOIN db.TB_MEAL M ON M.ID = DS.NIGHTMEALID
            WHERE DS.ID = {dishScheduleId}
            ";
            return new QueryModel(this.Query, null);
        }
    }
}
