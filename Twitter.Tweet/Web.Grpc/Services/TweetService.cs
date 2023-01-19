using System.Threading.Tasks;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Twitter.Web.Grpc.Protos.Tweet;
using Web.Grpc.Extensions;

namespace Web.Grpc.Services
{
    public class TweetService : TweetAction.TweetActionBase
    {
        private readonly ILogger<TweetService> _logger;
        private readonly IMediator _mediator;

        public TweetService(ILogger<TweetService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        
        public override async Task<MessageResponse> NewTweet(CreateTweetRequest request, ServerCallContext context)
        {
            
            await _mediator.Send(request.ToQuery());


            return new MessageResponse
            {
                Message = "added"
            };
        }
        
    }
}