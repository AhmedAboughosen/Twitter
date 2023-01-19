using System;
using Core.Domain.Enums;
using Core.Domain.Models.DTO.RequestDTO;
using Twitter.Web.Grpc.Protos.Tweet;

namespace Web.Grpc.Extensions
{
    public static class QueryExtensions
    {
        public static AddNewTweetRequestDto ToQuery(this CreateTweetRequest request)
            => new()
            {
                ContentTypes = request.GrpcTweetContentType switch
                {
                    GrpcTweetContentType.Image => TweetContentTypes.Image,
                    GrpcTweetContentType.Text => TweetContentTypes.Text,
                    _ => TweetContentTypes.Video
                },
                Content = request.Content,
                UserId = Guid.Parse(request.UserId),
            };
    }
}