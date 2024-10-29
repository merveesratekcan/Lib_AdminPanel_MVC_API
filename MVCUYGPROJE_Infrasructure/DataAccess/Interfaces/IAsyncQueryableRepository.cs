

namespace MVCUYGPROJE_Infrastructure.DataAccess.Interfaces;

public interface IAsyncQueryableRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync(bool tracking=true);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity,
        bool>> predicate,
        bool tracking = true);
    
}
