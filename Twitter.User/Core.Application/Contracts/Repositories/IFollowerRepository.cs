using System;
using System.Threading.Tasks;
using Core.Domain.Entities;

namespace Core.Application.Contracts.Repositories
{
    public interface IFollowerRepository : IAsyncRepository<Follower>
    {
        Task<bool> AnyAsync(Guid followerId, Guid followeeId);
        Task<Follower> FirstOrDefaultAsync(Guid followerId, Guid followeeId);
    }
}