using System;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        ITweetRepository TweetRepository { get; }
        IFollowerRepository FollowerRepository { get; }

        Task SaveChangesAsync();
    }
}