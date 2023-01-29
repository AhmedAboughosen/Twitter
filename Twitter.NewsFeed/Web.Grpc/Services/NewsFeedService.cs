using System.Threading.Tasks;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Twitter.Web.NewsFeed.Grpc.Protos.news_feed;
using Web.Grpc.Extensions;

namespace Web.Grpc.Services
{
    public class NewsFeedService : NewsFeed.NewsFeedBase
    {
        private readonly ILogger<NewsFeedService> _logger;
        private readonly IMediator _mediator;

        public NewsFeedService(ILogger<NewsFeedService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        public override async Task<LatestTweetListResponse> LatestTweet(CreateLatestTweetRequest request,
            ServerCallContext context)
        {
            var query = request.ToQuery();

            var response = await _mediator.Send(query);
            
            
            return response.ToIndexResponse();
        }
    }
}