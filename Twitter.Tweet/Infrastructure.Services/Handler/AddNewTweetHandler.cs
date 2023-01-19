using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Contracts.Repositories;
using Core.Application.Contracts.Services.Services.Publisher;
using Core.Domain.Entities;
using Core.Domain.Events;
using Core.Domain.Events.DataTypes;
using Core.Domain.Models.DTO.RequestDTO;
using MediatR;

namespace Infrastructure.Services.Handler
{
    public class AddNewTweetHandler : IRequestHandler<AddNewTweetRequestDto, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITweetInfoProducer _tweetInfoProducer;

        public AddNewTweetHandler(IUnitOfWork unitOfWork, ITweetInfoProducer tweetInfoProducer)
        {
            _unitOfWork = unitOfWork;
            _tweetInfoProducer = tweetInfoProducer;
        }


        public async Task<bool> Handle(AddNewTweetRequestDto dto,
            CancellationToken cancellationToken)
        {
            var tweet = new Tweet(dto);
            
            await _unitOfWork.TweetRepository.AddAsync(tweet);

            await _unitOfWork.SaveChangesAsync();

            await _tweetInfoProducer.SendMessageAsync(new MessageBody<TweetAddedData>()
            {
                Data = new TweetAddedData(tweet.Id, tweet.ContentType, tweet.Content, tweet.CreatedDate,tweet.UserId),
                Type = EventTypes.TweetAdded,
                AggregateId = tweet.Id.ToString(),
                Version = 1,
                Sequence = 1,
                DateTime = DateTime.UtcNow
            });

            
            return true;
        }
    }
}