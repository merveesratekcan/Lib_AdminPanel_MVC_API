

namespace MVCUYGPROJE_Infrastructure.DataAccess.Interfaces;

public interface IAsyncTransactionRepository
{
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task<IExecutionStrategy> CreateExecutionStrategy();
}
