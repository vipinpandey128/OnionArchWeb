using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Owner> GetByIdAsync(Guid ownerId, CancellationToken cancellationToken = default);

        void Insert(Owner owner);

        void Remove(Owner owner);
    }
}
