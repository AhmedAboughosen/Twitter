using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Contracts.Repositories;
using Core.Domain.Entities;
using Core.Domain.Events;
using Core.Domain.Events.DataTypes;
using Core.Domain.Exceptions;
using Core.Domain.Extensions;
using Infrastructure.Services.BaseService;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Services.Handler
{
    public class AddNewTweetHandler : IRequestHandler<MessageBody<TweetAddedData>, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache _distributedCache;
        private readonly TweetHub _tweetHub;

        public AddNewTweetHandler(IUnitOfWork unitOfWork, IDistributedCache distributedCache, TweetHub tweetHub)
        {
            _unitOfWork = unitOfWork;
            _distributedCache = distributedCache;
            _tweetHub = tweetHub;
        }


        public async Task<bool> Handle(MessageBody<TweetAddedData> dto,
            CancellationToken cancellationToken)
        {
            if (!(await _unitOfWork.UserRepository.AnyAsync(dto.Data.UserId)))
            {
                throw new APIException(
                    "user is not exists", HttpStatusCode.NotFound);
            }

            if ((await _unitOfWork.TweetRepository.AnyAsync(dto.Data.UserId)))
            {
                throw new APIException(
                    "tweet already added", HttpStatusCode.NotAcceptable);
            }

            var tweet = new Tweet(dto);

            await _unitOfWork.TweetRepository.AddAsync(tweet);


            //fetch all users which follow this user
            var followers = await _unitOfWork.FollowerRepository.GetFollowersAsync(tweet.UserId);

            foreach (var follower in followers)
            {
                var recordKey = follower + "_" + 1;

                var list = await _distributedCache.GetRecordAsync<List<Tweet>>(recordKey);

                list.Add(tweet);

                await _distributedCache.SetRecordAsync(recordKey, list, TimeSpan.FromHours(1),
                    TimeSpan.FromDays(3));
            }

            
            await _unitOfWork.SaveChangesAsync();

            //send notification to user
            foreach (var follower in followers)
            {

               await _tweetHub.SendTweetToUsers(follower,tweet);
            }

            return true;
        }
    }
}