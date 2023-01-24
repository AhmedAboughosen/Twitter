using System;
using MediatR;

namespace Core.Domain.Events.DataTypes
{
    public record NewFollowerAddedData(Guid Id, string FolloweeId, string FollowersId)
    {
        public NewFollowerAddedData() : this(default, default, default)
        {
        }
    }
}