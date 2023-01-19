using System;
using MediatR;

namespace Core.Domain.Model.DTO.RequestDTO
{
    public class UnFollowerRequestDto : IRequest<bool>
    {
        public Guid FollowerId { get; set; }
        public Guid FolloweeId { get; set; }
    }
}