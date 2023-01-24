using System;
using Core.Domain.Events;
using Core.Domain.Events.DataTypes;
using Core.Domain.Model.MessageBroker;

namespace Core.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public DateTime? LastLogin { get; private set; }

        public User(IMessageBody<UserCreatedData> dto)
        {
            FullName = dto.Data.FullName;
            Id =  Guid.Parse(dto.Data.Id);
            CreatedDate =  dto.Data.CreatedDate;
            LastLogin =  dto.Data.LastLogin;
        }

        public User()
        {
        }
    }
}