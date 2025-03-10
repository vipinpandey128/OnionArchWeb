using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<User>> GetAllByOwnerIdAsync(CancellationToken cancellationToken = default);

        Task<User> GetByIdAsync(Guid accountId, CancellationToken cancellationToken = default);

        void Insert(User account);

        void Remove(User account);
    }
}
