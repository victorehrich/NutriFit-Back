using NutriFit.Domain.Entities;
using NutriFit.Infrastructure.Shared.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Infrastructure.Input.Queries
{
    public class UserQuery : BaseQuery
    {
        public string Email { get; private set; }

        public string Name { get; private set; }

        public string Password { get; private set; }

        public int SexId { get; set; }

        public int BiotypeId { get; set; }

        public double Heigth { get; set; }

        public double Weigth { get; set; }

        public int Age { get; set; }

        public QueryModel InsertUserQuery(UserEntity user)
        {
            this.Table = Map.GetUserTable();
            this.Query = $@"
                INSERT INTO {this.Table}
                (
                    EMAIL,
                    NAME,
                    PASSWORD,
                    SEXID,
                    BIOTYPEID,
                    HEIGTH,
                    WEIGTH,
                    AGE,
                    CREATEDON
                )
                VALUES
                (
                    @Email,
                    @Name,
                    @Password,
                    @SexId,
                    @BiotypeId,
                    @Heigth,
                    @Weigth,
                    @Age,
                    @CreatedOn
                )
                ";
            this.Parameters = new
            {
                Email = user.Email,
                Name = user.Name, 
                Password = user.Password,
                SexId = user.SexId,
                BiotypeId = user.BiotypeId,
                Heigth = user.Heigth,
                Weigth = user.Weigth,
                Age = user.Age,
                CreatedOn = user.CreatedOn
            };
            return new QueryModel(this.Query, this.Parameters);
        }
    }
}
