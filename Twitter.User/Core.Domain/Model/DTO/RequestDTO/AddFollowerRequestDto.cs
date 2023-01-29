using System;
using MediatR;

namespace Core.Domain.Model.DTO.RequestDTO
{
    public class AddFollowerRequestDto : IRequest<bool>
    {
        //this user follow FolloweeId
        public Guid FollowerId { get; set; }
        //user id
        public Guid FolloweeId { get; set; }
    }
}