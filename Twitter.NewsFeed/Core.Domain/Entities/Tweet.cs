using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Enums;
using Core.Domain.Events.DataTypes;
using Core.Domain.Model.MessageBroker;

namespace Core.Domain.Entities
{
    public class Tweet
    {
        public Guid Id { get; private set; }

        public TweetContentTypes ContentType { get; private set; }

        public string Content { get; private set; }

        public DateTime CreatedDate { get; private set; }


        [ForeignKey("UserId")] public Guid UserId { get; private set; }
        public User User { get; private set; }


        public Tweet(IMessageBody<TweetAddedData> dto)
        {
            Id = Guid.NewGuid();
            ContentType = dto.Data.ContentType;
            Content = dto.Data.Content;
            CreatedDate = dto.Data.CreatedDate;
            UserId = dto.Data.UserId;
        }

        public Tweet()
        {
        }
    }
}