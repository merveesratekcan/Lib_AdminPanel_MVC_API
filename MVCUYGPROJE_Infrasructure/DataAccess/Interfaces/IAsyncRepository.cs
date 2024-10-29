

namespace MVCUYGPROJE_Infrastructure.DataAccess.Interfaces;

public interface IAsyncRepository
{
    Task<int> SaveChangesAsync();
}
