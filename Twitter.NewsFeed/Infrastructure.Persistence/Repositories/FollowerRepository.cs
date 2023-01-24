using System;
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

        public Task<Follower> FirstOrDefaultAsync(Guid followerId, Guid followeeId)
            => _appDbContext.Followers.FirstOrDefaultAsync(
                x => Guid.Parse(x.FollowersId) == followerId && Guid.Parse(x.FolloweeId) == followeeId);
    }
}