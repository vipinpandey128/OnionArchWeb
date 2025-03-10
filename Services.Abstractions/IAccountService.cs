using Contracts;

namespace Services.Abstractions
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDto>> GetAllByOwnerIdAsync(CancellationToken cancellationToken = default);

        Task<AccountDto> GetByIdAsync(Guid accountId, CancellationToken cancellationToken);

        Task<AccountDto> CreateAsync(AccountForCreationDto accountForCreationDto, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid accountId, CancellationToken cancellationToken = default);
    }
}
