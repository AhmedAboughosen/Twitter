using System;
using Core.Domain.Events;
using Newtonsoft.Json;
using Twitter.Tweet.Grpc.Protos.UserBuilder;

namespace Web.Grpc.Extensions
{
    public static class RebuildExtension
    {
        public static MessageBody<T> ToMessageBody<T>(this EventNotification eventMessage)
            => new()
            {
                AggregateId = eventMessage.Id,
                Sequence = 1,
                Type = eventMessage.Type,
                Data = JsonConvert.DeserializeObject<T>(eventMessage.ToString()),
                DateTime = DateTime.Parse(eventMessage.CreatedDate),
            };
    }
}