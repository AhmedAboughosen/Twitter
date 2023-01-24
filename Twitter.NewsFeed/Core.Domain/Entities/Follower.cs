using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Events.DataTypes;
using Core.Domain.Model.MessageBroker;

namespace Core.Domain.Entities
{
    public class Follower
    {
        public Guid Id { get; private set; }

        [ForeignKey("FolloweeId")]
        public string FolloweeId { get; private set; }
        public User Followee { get; private set; }
        
        
        [ForeignKey("FollowersId")]
        public string FollowersId { get; private set; }
        public User Followers { get; private set; }


        public Follower(IMessageBody<NewFollowerAddedData> dto)
        {
            FolloweeId = dto.Data.FolloweeId;
            FollowersId = dto.Data.FollowersId;
        }

        public Follower()
        {
        }
    }
}