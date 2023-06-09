﻿using NutriFit.Domain.Notifications;
using NutriFit.Domain.Validations;
namespace NutriFit.Domain.Entities
{
    public class UserEntity: BaseEntity
    {
        public int Id { get; private set; }
        List<Notification> _notifications;

        public string Email { get; private set; }

        public string Name { get; private set; }

        public string Password { get; private set; }

        public int SexId { get; private set; }

        public int BiotypeId { get; private set; }

        public double Heigth { get; private set; }

        public double Weigth { get; private set; }

        public int Age { get; private set; }

        public IReadOnlyCollection<Notification> Notifications => _notifications;

        public UserEntity(string Email,
                    string Name,
                    string Password,
                    int SexId,
                    int BiotypeId,
                    double Heigth,
                    double Weigth,
                    int Age)
        {

            this.Email = Email;
            this.Name = Name;
            this.Password = Password;
            this.SexId = SexId;
            this.BiotypeId = BiotypeId;
            this.Heigth = Heigth;
            this.Weigth = Weigth;
            this.Age = Age;
            _notifications = new List<Notification>();
            IsValid();
        }

        public UserEntity(
            int Id,
            string Email,
            string Name,
            string Password,
            int SexId,
            int BiotypeId,
            double Heigth,
            double Weigth,
            int Age)
        {
            this.Id = Id;
            this.Email = Email;
            this.Name = Name;
            this.Password = Password;
            this.SexId = SexId;
            this.BiotypeId = BiotypeId;
            this.Heigth = Heigth;
            this.Weigth = Weigth;
            this.Age = Age;
            _notifications = new List<Notification>();
            IsValid();
        }

        public bool IsValid()
        {
            return new UserValidations(this).EmailValidation().IsValid();
        }
        public void PullNotification(Notification notification)
        {
            this._notifications.Add(notification);
        }
    }
}
