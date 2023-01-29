using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Contracts.Repositories;
using Core.Domain.Entities;
using Core.Domain.Extensions;
using Core.Domain.Models;
using Core.Domain.Models.RequestDto;
using Core.Domain.Models.ResponseDto;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Services.Handler
{
    public class NewsFeedHandler : IRequestHandler<NewsFeedRequestDto, NewsFeedPaginationRequestDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDistributedCache _distributedCache;

        public NewsFeedHandler(IUnitOfWork unitOfWork, IDistributedCache distributedCache)
        {
            _unitOfWork = unitOfWork;
            _distributedCache = distributedCache;
        }


        public async Task<NewsFeedPaginationRequestDto> Handle(NewsFeedRequestDto dto,
            CancellationToken cancellationToken)
        {
            var userKey = dto.UserId.ToString();

            var newsFeedUserCachingDto = await _distributedCache.GetRecordAsync<NewsFeedUserCachingDto>(userKey);


            if (newsFeedUserCachingDto is null)
            {
                //No Caching

                //fetch followers of user
                var followers = await _unitOfWork.FollowerRepository.GetAllAsync(dto.UserId);

                List<Tweet> tweetList = new List<Tweet>();

                foreach (var follower in followers)
                {
                    var tweet = await _unitOfWork.TweetRepository.GetAsync(Guid.Parse(follower.FollowersId));
                    tweetList.AddRange(tweet);
                }

                //ranking algorithm based On Date.

                List<Tweet> sortedTweetList = tweetList.OrderBy(o => o.CreatedDate).ToList();

                int index = 1;
                int pageNumber = 1;
                List<Tweet> savedTweetList = new List<Tweet>();

                for (var i = 0; i > sortedTweetList.Count; i++)
                {
                    if (index == 10)
                    {
                        var recordKey = dto.UserId + "_" + pageNumber;

                        await _distributedCache.SetRecordAsync(recordKey, savedTweetList, TimeSpan.FromHours(1),
                            TimeSpan.FromDays(3));

                        pageNumber++;
                        index = 1;
                        continue;
                    }

                    savedTweetList.Add(savedTweetList[i]);
                    index++;
                }

                if (savedTweetList.Count != 0)
                {
                    var recordKey = dto.UserId + "_" + pageNumber;

                    await _distributedCache.SetRecordAsync(recordKey, sortedTweetList, TimeSpan.FromHours(1),
                        TimeSpan.FromDays(3));
                }

                newsFeedUserCachingDto = new NewsFeedUserCachingDto
                {
                    TotalPages = savedTweetList.Count / 10,
                    UserId = dto.UserId
                };

                await _distributedCache.SetRecordAsync(userKey, newsFeedUserCachingDto, TimeSpan.FromHours(1),
                    TimeSpan.FromDays(3));
            }

            var key = dto.UserId + "_" + dto.PageNumber;

            var list = await _distributedCache.GetRecordAsync<List<Tweet>>(key);


            return new NewsFeedPaginationRequestDto
            {
                Total = newsFeedUserCachingDto.TotalPages,
                CurrentPage = dto.PageNumber,
                PageSize = dto.PageSize,
                PageContent = list.Select(o => new TweetResponseDto
                {
                    Content = o.Content,
                    ContentType = o.ContentType,
                    CreatedDate = o.CreatedDate,
                    TweetId = o.Id,
                    UserId = o.UserId,
                }).ToList()
            };
        }
    }
}