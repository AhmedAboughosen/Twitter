using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Application.Contracts.Repositories;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class FollowerRepository : AsyncRepository<Follower>, IFollowerRepository
    {
        private readonly AppDbContext _appDbContext;

        public FollowerRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<bool> AnyAsync(Guid followerId, Guid followeeId)
            => _appDbContext.Followers.AnyAsync(x =>
                Guid.Parse(x.FollowersId) == followerId && Guid.Parse(x.FolloweeId) == followeeId);

        public Task<List<Follower>> GetAllAsync(Guid userId)
            => _appDbContext.Followers.Where(x => Guid.Parse(x.FolloweeId) == userId).ToListAsync();


        public Task<List<Guid>> GetFollowersAsync(Guid userId)
            => _appDbContext.Followers.Where(x => Guid.Parse(x.FollowersId) == userId).Select(o => Guid.Parse(o.FollowersId))
                .ToListAsync();


        public Task<Follower> FirstOrDefaultAsync(Guid followerId, Guid followeeId)
            => _appDbContext.Followers.FirstOrDefaultAsync(
                x => Guid.Parse(x.FollowersId) == followerId && Guid.Parse(x.FolloweeId) == followeeId);
    }
}