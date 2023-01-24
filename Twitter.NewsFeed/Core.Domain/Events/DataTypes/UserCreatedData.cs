using System;

namespace Core.Domain.Events.DataTypes
{
    public record UserCreatedData(string Id, string FullName, DateTime CreatedDate, DateTime? LastLogin)
    {
        public UserCreatedData() : this(default, default, default, default)
        {
        }
    }
}