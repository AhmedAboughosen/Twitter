using System.Threading.Tasks;
using Core.Application.Contracts.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        private IFollowerRepository _followerRepository;

        public IFollowerRepository FollowerRepository
        {
            get
            {
                if (_followerRepository != null)
                    return _followerRepository;
                return _followerRepository = new FollowerRepository(_appDbContext);
            }
        }


        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }


        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}