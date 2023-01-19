using System.Threading.Tasks;
using Core.Application.Contracts.Services.Services.BaseService;
using Core.Domain.Events;
using Core.Domain.Events.DataTypes;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Twitter.Tweet.Grpc.Protos.Rebuild;
using Twitter.Tweet.Grpc.Protos.UserBuilder;
using Web.Grpc.Extensions;

namespace Web.Grpc.Services
{
    public class RebuildService : Rebuild.RebuildBase
    {
        private readonly ILogger<RebuildService> _logger;
        private readonly IMediator _mediator;
        private readonly UserHistory.UserHistoryClient _client;
        private readonly IMessageHandler _messageHandler;

        public RebuildService(ILogger<RebuildService> logger, IMediator mediator, UserHistory.UserHistoryClient client,
            IMessageHandler messageHandler)
        {
            _logger = logger;
            _mediator = mediator;
            _client = client;
            _messageHandler = messageHandler;
        }


        public override async Task<Empty> BuildUsers(BuildRequest request, ServerCallContext context)
        {
            var buildPage = new BuildPage(request);

            for (int i = buildPage.Page; i > 0; i++)
            {
                var response = await _client.ReadAsync(new Request()
                {
                    Page = i,
                    Size = buildPage.Size
                });

                if (response.EventNotification.Count > 0)
                {
                    await HandelResponseAsync(response, i);
                }
                else
                    break;
            }

            return new Empty();
        }

        private async Task HandelResponseAsync(Response response, int page)
        {
            foreach (var @event in response.EventNotification)
            {
                switch (@event.Type)
                {
                    case EventTypes.UserCreated:
                        await _messageHandler.HandleAsync(@event.ToMessageBody<UserCreatedData>());
                        break;
                }
            }
        }


        private class BuildPage
        {
            public BuildPage(BuildRequest request, int defaultSize = 100)
            {
                Page = request.Page ?? 1;
                Size = request.Size ?? defaultSize;
            }

            public int Page { get; }
            public int Size { get; }
        }
    }
}