using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Application.Contracts.Repositories;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class TweetRepository : AsyncRepository<Tweet>, ITweetRepository
    {
        private readonly AppDbContext _appDbContext;

        public TweetRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<bool> AnyAsync(Guid tweetId)
            => _appDbContext.Tweets.AnyAsync(x => x.Id == tweetId);
        
        public Task<List<Tweet>> GetAsync(Guid userId)
            => _appDbContext.Tweets.Where(x => x.UserId == userId).ToListAsync();
    }
}