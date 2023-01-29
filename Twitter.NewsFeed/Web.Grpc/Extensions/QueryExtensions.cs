using System;
using Core.Domain.Models.RequestDto;
using Twitter.Web.NewsFeed.Grpc.Protos.news_feed;

namespace Web.Grpc.Extensions
{
    public static class QueryExtensions
    {
        public static NewsFeedRequestDto ToQuery(this CreateLatestTweetRequest request)
            => new()
            {
              PageNumber = request.PageNumber,
              PageSize = request.PageSize,
              UserId = Guid.Parse(request.UserId)
            };

      
    }
}