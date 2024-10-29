using MVCUYGPROJE_Domain.Entities;
using MVCUYGPROJE_Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCUYGPROJE_Infrastructure.Repositories.AuthorRepository
{
    public interface IAuthorRepository : IAsyncRepository, IAsyncInsertable<Author>, IAsyncFindableRepository<Author>,
  IAsyncQueryableRepository<Author>, IAsyncUpdatableRepository<Author>, IAsyncDeletableRepository<Author>
    {
    }
}
