using System;
using MediatR;

namespace Core.Domain.Events.DataTypes
{
    public record UnFollowerAddedData(Guid Id, string FolloweeId, string FollowersId)
    {
        public UnFollowerAddedData() : this(default, default, default)
        {
        }
    }
}