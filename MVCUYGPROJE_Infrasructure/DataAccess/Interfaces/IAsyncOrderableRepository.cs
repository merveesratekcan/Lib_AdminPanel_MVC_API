

namespace MVCUYGPROJE_Infrastructure.DataAccess.Interfaces;

public interface IAsyncOrderableRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity,TKey>> orderBy,
        bool orderByDesc,
        bool tracking=true);

    //default değerli parametre en sonda olur.
    Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity,
        bool>> expression, 
        Expression<Func<TEntity, TKey>> orderBy,
        bool orderByDesc, bool tracking = true);

}
