using Core.Application.Contracts.Repositories;
using Core.Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class TweetRepository : AsyncRepository<Tweet>, ITweetRepository
    {
        private readonly AppDbContext _appDbContext;

        public TweetRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}