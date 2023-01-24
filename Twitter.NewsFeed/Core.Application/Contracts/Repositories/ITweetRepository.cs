
using System;
using System.Threading.Tasks;
using Core.Domain.Entities;

namespace Core.Application.Contracts.Repositories
{
    public interface ITweetRepository : IAsyncRepository<Tweet>
    {
        Task<bool> AnyAsync(Guid tweetId);

    }
}