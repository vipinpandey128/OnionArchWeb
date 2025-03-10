using Domain.Repositories;
using Services.Abstractions;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAccountService> _lazyAccountService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _lazyAccountService = new Lazy<IAccountService>(() => new AccountService(repositoryManager));
        }


        public IAccountService AccountService => _lazyAccountService.Value;
    }
}
