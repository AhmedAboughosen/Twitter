using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain.Events.DataTypes;
using Core.Domain.Model.MessageBroker;

namespace Core.Domain.Entities
{
    public class Follower
    {
        public Guid Id { get; private set; }


        //Current User Id
        [ForeignKey("FolloweeId")] public string FolloweeId { get; private set; }
        public User Followee { get; private set; }

        //User Id Follow this user 
        [ForeignKey("FollowersId")] public string FollowersId { get; private set; }
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