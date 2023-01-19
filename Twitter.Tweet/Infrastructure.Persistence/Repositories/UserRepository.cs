using Core.Application.Contracts.Repositories;
using Core.Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : AsyncRepository<User>, IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}