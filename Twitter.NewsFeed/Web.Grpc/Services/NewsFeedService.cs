using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Twitter.Web.NewsFeed.Grpc.Protos.news_feed;

namespace Web.Grpc.Services
{
    public class NewsFeedService : NewsFeed.NewsFeedBase
    {
        private readonly ILogger<NewsFeedService> _logger;

        public NewsFeedService(ILogger<NewsFeedService> logger)
        {
            _logger = logger;
        }


        public override Task<LatestTweetListResponse> LatestTweet(CreateLatestTweetRequest request, ServerCallContext context)
        {
            return base.LatestTweet(request, context);
        }
    }
}