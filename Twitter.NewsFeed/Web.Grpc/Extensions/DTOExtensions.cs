using System.Linq;
using Core.Domain.Models.ResponseDto;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Twitter.Web.NewsFeed.Grpc.Protos.news_feed;

namespace Web.Grpc.Extensions
{
    public static class DtoExtensions
    {
        public static LatestTweetListResponse ToIndexResponse(this NewsFeedPaginationRequestDto response)
        {

            RepeatedField<LatestTweetResponse> tweetListResponses = new RepeatedField<LatestTweetResponse>();
            
            return new LatestTweetListResponse
            {
              PageNumber = response.CurrentPage,
              PageSize = response.PageSize,
              TotalPages = response.Total,
              LatestTweet = tweetListResponses
            };
        }
    }
}