using System;
using Core.Domain.Enums;

namespace Core.Domain.Events.DataTypes
{
    public record TweetAddedData(Guid Id, TweetContentTypes ContentType, string Content, DateTime CreatedDate,
        Guid UserId)
    {
        public TweetAddedData() : this(default, default, default, default, default)
        {
        }
    }
}