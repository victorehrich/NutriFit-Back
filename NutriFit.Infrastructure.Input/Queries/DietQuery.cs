using Newtonsoft.Json;
using NutriFit.Domain.Entities;
using NutriFit.Infrastructure.Shared.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NutriFit.Infrastructure.Input.Queries
{
    public class DietQuery : BaseQuery
    {
        public QueryModel InsertMealQuery(MealEntity meal)
        {
            this.Table = Map.GetMealTable();
            this.Query = $@"
                INSERT INTO {this.Table}
                (
                    DISH,
                    STARTTIME,
                    ENDTIME
                )
                VALUES
                (
                    @Dish,
                    @StartTime,
                    @EndTime
                );
                SELECT LAST_INSERT_ID();
                ";
            this.Parameters = new
            {
                Dish = System.Text.Json.JsonSerializer.Serialize<IList<DishEntity>>(meal.Dish),
                StartTime = meal.StartTime,
                EndTime = meal.EndTime
            };
            return new QueryModel(this.Query, this.Parameters);
        }

        public QueryModel InsertDietScheduleQuery(DishScheduleEntity dishScheduleEntity)
        {
            this.Table = Map.GetDietScheduleTable();
            this.Query = $@"
                INSERT INTO {this.Table}
                (
                    MORNINGMEALID,
                    AFTERNOONMEALID,
                    NIGHTMEALID
                )
                VALUES
                (
                    @MorningMealId,
                    @AfternoonMealId,
                    @NightnMealId
                );
                SELECT LAST_INSERT_ID();
                ";
            this.Parameters = new
            {
                MorningMealId = dishScheduleEntity.MorningMealId,
                AfternoonMealId = dishScheduleEntity.AfternoonMealId,
                NightnMealId = dishScheduleEntity.NightnMealId
            };
            return new QueryModel(this.Query, this.Parameters);
        }
        public QueryModel InsertDietQuery(DietEntity diet)
        {
            this.Table = Map.GetDietTable();
            this.Query = $@"
                INSERT INTO {this.Table}
                (
                    CURRENTACTIVE,
                    DIETNAME,
                    DIETGOAL,
                    MONDAYSCHEDULEID,
                    TUESDAYSCHEDULEID,
                    WEDNESDAYSCHEDULEID,
                    THURSDAYSCHEDULEID, 
                    FRIDAYSCHEDULEID,
                    SATURDAYSCHEDULEID,
                    SUNDAYSCHEDULEID,
                    CREATEDON
                )
                VALUES
                (
                    @CurrentActive,
                    @DietName,
                    @DietGoal,
                    @MondayScheduleId,
                    @TuesdayScheduleId,
                    @WednesdayScheduleId,
                    @ThursdayScheduleId,
                    @FridayScheduleId,
                    @SaturdayScheduleId,
                    @SundayScheduleId,
                    @CreatedOn

                );
                SELECT LAST_INSERT_ID();
                ";
            this.Parameters = new
            {
                CurrentActive = diet.CurrentActive,
                DietName = diet.DietName,
                DietGoal = diet.DietGoal,
                MondayScheduleId = diet.MondayScheduleId,
                TuesdayScheduleId = diet.TuesdayScheduleId,
                WednesdayScheduleId = diet.WednesdayScheduleId,
                ThursdayScheduleId = diet.ThursdayScheduleId,
                FridayScheduleId = diet.FridayScheduleId,
                SaturdayScheduleId = diet.SaturdayScheduleId,
                SundayScheduleId = diet.SundayScheduleId,
                CreatedOn = diet.CreatedOn 
            };
            return new QueryModel(this.Query, this.Parameters);
        }
        public QueryModel UpdateOldDietsStatusQuery(int userId)
        {
            this.InnerTable = new List<string>();
            this.Table = Map.GetDietTable();
            this.InnerTable.Add(Map.GetUserDietTable());
            this.Query = $@"
            UPDATE 
                {this.Table} D
            JOIN 
                {this.InnerTable[0]} UD
            ON 
                D.ID = UD.DIETID
            SET 
                D.CURRENTACTIVE = FALSE
            WHERE
                D.CURRENTACTIVE = TRUE
            AND
                UD.USERID = {userId}
            ";
            return new QueryModel(this.Query, null);
        }

        public QueryModel InsertUserDietQuery(UserDietEntity userDiet)
        {
            this.Table = Map.GetUserDietTable();
            this.Query = $@"
                INSERT INTO {this.Table}
                (
                    USERID,
                    DIETID
                )
                VALUES
                (
                    @UserId,
                    @DietId

                );
                SELECT LAST_INSERT_ID();
                ";
            this.Parameters = new
            {
                UserId = userDiet.UserId,
                DietId = userDiet.DietId
            };
            return new QueryModel(this.Query, this.Parameters);
        }

        public QueryModel UpdateDietStatusQuery(int dietId)
        {
            this.Table = Map.GetDietTable();
            this.Query = $@"
                UPDATE {this.Table}
                SET 
                    CURRENTACTIVE = TRUE
                WHERE
                    ID = @DietId
                ";
            this.Parameters = new
            {
                DietId = dietId
            };
            return new QueryModel(this.Query, this.Parameters);
        }
    }
}
