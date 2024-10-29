
namespace MVCUYGPROJE_Infrastructure.DataAccess.Interfaces;

//tekli gönderme bulma işlemi - any
public interface IAsyncFindableRepository<TEntity> where TEntity : BaseEntity
{
    Task<bool> AnyAsync(Expression<Func<TEntity,bool>> exception=null);
    Task<TEntity?> GetByIdAsync(Guid id,bool tracking=true);
    //tracking = true ise veri takip edilir.
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> exception, bool tracking = true);

}
