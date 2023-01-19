using System;
using Core.Domain.Enums;
using MediatR;

namespace Core.Domain.Models.DTO.RequestDTO
{
    public class AddNewTweetRequestDto : IRequest<bool>
    {
        public TweetContentTypes ContentTypes { get; set; }

        public string Content { get; set; }

        public Guid UserId { get; set; }
    }
}