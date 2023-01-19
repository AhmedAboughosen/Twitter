using System;
using System.Threading.Tasks;

namespace Core.Application.Contracts.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IFollowerRepository FollowerRepository { get; }

        Task SaveChangesAsync();
    }
}