using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Repositories
{
    internal sealed class AccountRepository : IAccountRepository
    {
        private readonly RepositoryDbContext _dbContext;

        public AccountRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<User>> GetAllByOwnerIdAsync(CancellationToken cancellationToken = default) =>
            await _dbContext.Accounts.ToListAsync(cancellationToken);

        public async Task<User> GetByIdAsync(Guid accountId, CancellationToken cancellationToken = default) =>
            await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == accountId, cancellationToken);

        public void Insert(User account) => _dbContext.Accounts.Add(account);

        public void Remove(User account) => _dbContext.Accounts.Remove(account);
    }
}
