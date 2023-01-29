using System;

namespace Core.Domain.Models
{
    public class NewsFeedUserCachingDto
    {
        public NewsFeedUserCachingDto()
        {
        }

        public Guid UserId { get; set; }
        public int TotalPages { get; set; }
        
    }
    
}