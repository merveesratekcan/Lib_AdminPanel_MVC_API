

namespace MVCUYGPROJE_Infrastructure.Repositories.AdminRepository;

public interface IAdminRepository : IAsyncRepository, IAsyncInsertable<Admin>, IAsyncFindableRepository<Admin>,
IAsyncQueryableRepository<Admin>, IAsyncUpdatableRepository<Admin>, IAsyncDeletableRepository<Admin>,IAsyncTransactionRepository
{
    Task<Admin?> GetByIdentityId(string identityId);
}
