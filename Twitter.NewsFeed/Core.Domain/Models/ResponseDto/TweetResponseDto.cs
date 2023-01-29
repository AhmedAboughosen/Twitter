using System;
using Core.Domain.Enums;

namespace Core.Domain.Models.ResponseDto
{
    public class TweetResponseDto
    {
        public TweetResponseDto()
        {
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        
        
        public Guid TweetId { get;  set; }

        public TweetContentTypes ContentType { get;  set; }

        public string Content { get;  set; }

        public DateTime CreatedDate { get;  set; }
    }
    
}