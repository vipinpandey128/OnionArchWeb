using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Services.Abstractions;

namespace Services
{
    internal sealed class AccountService : IAccountService
    {
        private readonly IRepositoryManager _repositoryManager;

        public AccountService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

        public async Task<IEnumerable<AccountDto>> GetAllByOwnerIdAsync(CancellationToken cancellationToken = default)
        {
            var accounts = await _repositoryManager.AccountRepository.GetAllByOwnerIdAsync(cancellationToken);

            var accountsDto = accounts.Adapt<IEnumerable<AccountDto>>();

            return accountsDto;
        }

        public async Task<AccountDto> GetByIdAsync(Guid accountId, CancellationToken cancellationToken)
        {
            var account = await _repositoryManager.AccountRepository.GetByIdAsync(accountId, cancellationToken);

            if (account is null)
            {
                throw new AccountNotFoundException(accountId);
            }

            var accountDto = account.Adapt<AccountDto>();

            return accountDto;
        }

        public async Task<AccountDto> CreateAsync(AccountForCreationDto accountForCreationDto, CancellationToken cancellationToken = default)
        {
            var account = accountForCreationDto.Adapt<User>();

            _repositoryManager.AccountRepository.Insert(account);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return account.Adapt<AccountDto>();
        }

        public async Task DeleteAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
            var account = await _repositoryManager.AccountRepository.GetByIdAsync(accountId, cancellationToken);

            if (account is null)
            {
                throw new AccountNotFoundException(accountId);
            }

            _repositoryManager.AccountRepository.Remove(account);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
