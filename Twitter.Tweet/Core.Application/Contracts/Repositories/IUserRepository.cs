using Core.Domain.Entities;

namespace Core.Application.Contracts.Repositories
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}