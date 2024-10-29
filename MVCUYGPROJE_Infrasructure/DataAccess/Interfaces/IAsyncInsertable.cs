

namespace MVCUYGPROJE_Infrastructure.DataAccess.Interfaces;

public interface IAsyncInsertable<TEntity>:IAsyncRepository where TEntity : BaseEntity
{
   Task<TEntity> AddAsync(TEntity entity);
   Task AddRangeAsync(IEnumerable<TEntity> entities);

}
